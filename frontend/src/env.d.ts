interface ImportMetaEnv {
  readonly VITE_ENVIRONMENT: string;
  readonly VITE_URL_API: string;
}

interface ImportMeta {
  readonly env: ImportMetaEnv;
}

declare module '*.vue';
