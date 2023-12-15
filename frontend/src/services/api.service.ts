import { Axios } from 'axios-observable';

export default class ApiClient {
  client: Axios;

  constructor(url: string) {
    console.log(url);
    this.client = Axios.create({
      baseURL: url,
      headers: {
        Accept: '*/*',
        'Content-Type': 'application/json',
      },
    });
  }

  getUserById() {
    console.log();
    return this.client.get('/user/1');
  }
}
