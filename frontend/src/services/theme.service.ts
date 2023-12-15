import { BehaviorSubject } from 'rxjs';

export default class ThemeService {
  theme$ = new BehaviorSubject<string>('light');

  change(theme: string) {
    this.theme$.next(theme);
  }

  getCurrentTheme() {
    return this.theme$.getValue();
  }
}
