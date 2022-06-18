import { Component, OnInit, ChangeDetectionStrategy, Input } from '@angular/core';
import { AuditlogItemDto, AuditlogStatusEnum, AuditlogTypeEnum } from '@api/api.generated.clients';

@Component({
  selector: 'app-auditlog-item',
  templateUrl: './auditlog-item.component.html',
  styleUrls: ['./auditlog-item.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AuditlogItemComponent implements OnInit {
  @Input()
  public item: AuditlogItemDto | undefined;

  public AuditlogStatusEnum = AuditlogStatusEnum;
  public AuditlogTypeEnum = AuditlogTypeEnum;

  constructor() {}

  ngOnInit(): void {}
}