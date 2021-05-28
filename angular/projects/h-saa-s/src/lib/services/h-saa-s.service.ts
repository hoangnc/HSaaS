import { Injectable } from '@angular/core';
import { RestService } from '@abp/ng.core';

@Injectable({
  providedIn: 'root',
})
export class HSaaSService {
  apiName = 'HSaaS';

  constructor(private restService: RestService) {}

  sample() {
    return this.restService.request<void, any>(
      { method: 'GET', url: '/api/HSaaS/sample' },
      { apiName: this.apiName }
    );
  }
}
