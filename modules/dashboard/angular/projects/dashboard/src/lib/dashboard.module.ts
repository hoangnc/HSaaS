import { NgModule, NgModuleFactory, ModuleWithProviders } from '@angular/core';
import { CoreModule, LazyModuleFactory } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { DashboardComponent } from './components/dashboard.component';
import { DashboardRoutingModule } from './dashboard-routing.module';

@NgModule({
  declarations: [DashboardComponent],
  imports: [CoreModule, ThemeSharedModule, DashboardRoutingModule],
  exports: [DashboardComponent],
})
export class DashboardModule {
  static forChild(): ModuleWithProviders<DashboardModule> {
    return {
      ngModule: DashboardModule,
      providers: [],
    };
  }

  static forLazy(): NgModuleFactory<DashboardModule> {
    return new LazyModuleFactory(DashboardModule.forChild());
  }
}
