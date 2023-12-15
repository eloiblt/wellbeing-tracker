// noinspection ES6RedundantAwait

import { config, RouterLinkStub } from '@vue/test-utils';
import { createI18n } from 'vue-i18n';
import LoadingService from '../../src/services/loading.service.ts';
import ApiClient from '../../src/services/api.service.ts';
import { createTestingPinia } from '@pinia/testing';
import messages from '@intlify/unplugin-vue-i18n/messages';
import { afterAll, afterEach, beforeAll } from 'vitest';
import { routes } from '../../src/router.ts';
import { vClickOutside } from '../../src/directives/click-outside.directive.ts';
import { rest } from 'msw';
import { setupServer } from 'msw/node';

config.global.plugins = [
  createI18n({
    locale: 'en',
    messages,
  }),
  createTestingPinia,
  routes(),
];

config.global.stubs = {
  RouterLink: RouterLinkStub,
};

config.global.mocks = {
  t: tKey => tKey,
};

config.global.provide = {
  loadingService: new LoadingService(),
  apiClient: new ApiClient('http://localhost:9000'),
};

config.global.directives = {
  clickOutside: vClickOutside,
};

const restHandlers = [
  rest.post('*/audit/admin-page', async (_req, res, ctx) => {
    return await res(ctx.status(200));
  }),
];

const server = setupServer(...restHandlers);

beforeAll(() => {
  server.listen();
});

afterEach(() => {
  server.resetHandlers();
});

afterAll(() => {
  server.close();
});
