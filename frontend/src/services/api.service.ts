import { Axios } from 'axios-observable';
import { of, pipe, switchMap, throwError } from 'rxjs';

export default class ApiClient {
  client: Axios;

  constructor(url: string) {
    this.client = Axios.create({
      baseURL: url,
      headers: {
        Accept: '*/*',
        'Content-Type': 'application/json',
      },
      validateStatus: () => true,
    });
  }

  getCurrentUser() {
    return this.client.get(`/user/current`).pipe(this.manageErrors());
  }

  getUserById(id: number) {
    return this.client.get(`/user/${id}`).pipe(this.manageErrors());
  }

  login(data: { email: string; password: string }) {
    return this.client.post<string>('/user/login', data).pipe(this.manageErrors());
  }

  signup(data: { firstName: string; lastName: string; email: string; password: string }) {
    return this.client.post<string>('/user/signup', data).pipe(this.manageErrors());
  }

  manageErrors() {
    return pipe(switchMap((res: any) => (res.status < 200 || res.status >= 300 ? throwError(() => res) : of(res))));
  }
}
