import { Injectable } from '@angular/core';
import { RestService } from '@abp/ng.core';

@Injectable({
  providedIn: 'root',
})
export class DashboardService {
  apiName = 'Dashboard';

  constructor(private restService: RestService) {}

  sample() {
    return this.restService.request<void, any>(
      { method: 'GET', url: '/api/Dashboard/sample' },
      { apiName: this.apiName }
    );
  }
}
