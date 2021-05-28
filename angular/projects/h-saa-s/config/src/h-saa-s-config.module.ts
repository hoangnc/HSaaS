import { ModuleWithProviders, NgModule } from '@angular/core';
import { H_SAA_S_ROUTE_PROVIDERS } from './providers/route.provider';

@NgModule()
export class HSaaSConfigModule {
  static forRoot(): ModuleWithProviders<HSaaSConfigModule> {
    return {
      ngModule: HSaaSConfigModule,
      providers: [H_SAA_S_ROUTE_PROVIDERS],
    };
  }
}
