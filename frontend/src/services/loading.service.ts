import { BehaviorSubject } from 'rxjs';

export default class LoadingService {
  loading$ = new BehaviorSubject(false);

  startLoading() {
    this.loading$.next(true);
  }

  stopLoading() {
    this.loading$.next(false);
  }
}
