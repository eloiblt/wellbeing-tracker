import { type Router } from 'vue-router';
import { BehaviorSubject } from 'rxjs';
import { type Axios } from 'axios-observable';

export default class AuthService {
  loggedSubject = new BehaviorSubject<boolean>(!!localStorage.getItem('token'));
  router: Router;
  client: Axios;

  get logged() {
    return this.loggedSubject.value;
  }

  constructor(client: Axios, router: Router) {
    this.router = router;
    this.client = client;

    this.interceptRequests();
    this.interceptResponses();
  }

  interceptRequests() {
    this.client.interceptors.request.use(
      async config => {
        const token = localStorage.getItem('token');
        if (token) {
          config.headers!.Authorization = 'Bearer ' + token;
        } else {
          await this.logout();
        }
        return config;
      },
      async error => {
        return await Promise.reject(error);
      }
    );
  }

  interceptResponses() {
    this.client.interceptors.response.use(
      res => res,
      async err => {
        if (err.response.status === 401) {
          await this.logout();
        }
      }
    );
  }

  async login(token: string) {
    localStorage.setItem('token', token);
    this.loggedSubject.next(true);
    await this.router.push('/');
  }

  async logout() {
    localStorage.removeItem('token');
    this.loggedSubject.next(false);
    await this.router.push('/login');
  }
}
