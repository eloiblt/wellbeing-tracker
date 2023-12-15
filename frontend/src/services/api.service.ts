import { Axios } from 'axios-observable';

export default class ApiClient {
  client: Axios;

  constructor(url: string) {
    this.client = Axios.create({
      baseURL: url,
      headers: {
        Accept: '*/*',
        'Content-Type': 'application/json',
      },
    });
  }

  getUserById() {
    return this.client.get('/user/1');
  }

  login(data: { email: string; password: string }) {
    return this.client.post<string>('/user/login', data);
  }
}
