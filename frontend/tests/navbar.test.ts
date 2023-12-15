import { mount } from '@vue/test-utils';
import { expect, it } from 'vitest';
import LanguageSelector from '../src/components/shared/LanguageSelector.vue';

it('Language change is working', async () => {
  const navBarWrapper = mount(LanguageSelector, {
    attachTo: document.body,
  });

  const languageChangeBtn = navBarWrapper.find('button');
  expect(languageChangeBtn.text()).toBe('FR');
  await languageChangeBtn.trigger('click');
  expect(languageChangeBtn.text()).toBe('EN');
});
