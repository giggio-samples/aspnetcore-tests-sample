import { Injectable, Injector, NgZone } from '@angular/core';
import { Router } from '@angular/router';

/**
 * Service that allows Angular components to navigate from outside
 * Usage:
 *   window.ngNavigate('route')
 */
@Injectable({
  providedIn: 'root'
})
export class OutsideRouterService {
  constructor(private zone: NgZone, private injector: Injector) {}

  public setup() {
    if ((window as any).ngNavigate) return;
    (window as any).ngNavigate = async (route: string) => {
      await this.zone.run(async () => {
        const router = this.injector.get<Router>(Router);
        await router.navigateByUrl(route);
      });
    };
  }
}
