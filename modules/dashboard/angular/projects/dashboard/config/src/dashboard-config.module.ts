import { ModuleWithProviders, NgModule } from '@angular/core';
import { DASHBOARD_ROUTE_PROVIDERS } from './providers/route.provider';

@NgModule()
export class DashboardConfigModule {
  static forRoot(): ModuleWithProviders<DashboardConfigModule> {
    return {
      ngModule: DashboardConfigModule,
      providers: [DASHBOARD_ROUTE_PROVIDERS],
    };
  }
}
