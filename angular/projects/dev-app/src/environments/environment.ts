import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: false,
  application: {
    baseUrl: 'http://localhost:4200/',
    name: 'HSaaS',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44352',
    redirectUri: baseUrl,
    clientId: 'HSaaS_App',
    responseType: 'code',
    scope: 'offline_access HSaaS role email openid profile',
  },
  apis: {
    default: {
      url: 'https://localhost:44352',
      rootNamespace: 'HSaaS',
    },
    HSaaS: {
      url: 'https://localhost:44325',
      rootNamespace: 'HSaaS',
    },
  },
} as Environment;
