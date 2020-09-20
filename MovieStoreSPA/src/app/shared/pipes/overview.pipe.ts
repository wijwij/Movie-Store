import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'overview',
})
export class OverviewPipe implements PipeTransform {
  transform(overview: string): string {
    if (!overview) return;
    return overview.length <= 200 ? overview : overview.substr(0, 200) + '...';
  }
}
