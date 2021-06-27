import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: true,
  application: {
    baseUrl: 'http://localhost:4200/',
    name: 'Dashboard',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44329',
    redirectUri: baseUrl,
    clientId: 'Dashboard_App',
    responseType: 'code',
    scope: 'offline_access Dashboard',
  },
  apis: {
    default: {
      url: 'https://localhost:44329',
      rootNamespace: 'HSaaS.Dashboard',
    },
    Dashboard: {
      url: 'https://localhost:44316',
      rootNamespace: 'HSaaS.Dashboard',
    },
  },
} as Environment;
