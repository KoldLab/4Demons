import { ColumnDef } from '@tanstack/react-table';
import { useTranslation } from 'react-i18next';
import { ArrowDown, ArrowUp, ArrowUpDown } from 'lucide-react';
import { Button } from '@/components/ui/button';
import { Badge } from '@/components/ui/badge';
import { Link } from 'react-router';
import { useEffect, useMemo, useState } from 'react';
import { Campaign, CampaignDashboardFilter } from '@/type/campaigns/type';
import useLanguageStore from '@/stores/languageStore';
import { filter } from 'lodash';

// --- Formatting helpers ---
const formatNumber = (value?: number) =>
  value != null ? value.toLocaleString() : '—';
const formatPercent = (value?: number) =>
  value != null ? `${value.toFixed(2)}%` : '—';
const formatDate = (value?: string) => {
  if (!value) return '—';
  const d = new Date(value);
  return isNaN(d.getTime()) ? '—' : d.toLocaleDateString();
};

// === ALERT HELPERS (ASP-style) ===
function getAlertClassAndTitle(
  alertsList: { Id: string; En: string; Fr: string; CampaignProperty: string }[],
  columnKey: string,
  rowAlerts: string | undefined,
  lang: string
): { className: string; title: string } {
  if (!rowAlerts) return { className: '', title: '' };

  const triggered = alertsList.filter(
    (a) => rowAlerts.includes(`;${a.Id};`) && a.CampaignProperty === columnKey
  );

  if (triggered.length === 0) return { className: '', title: '' };

  const title = triggered
    .map((a) => (lang?.toLowerCase().startsWith('fr') ? a.Fr : a.En))
    .join(', ');

  // match ASP .hasAlert color (orangered); keep it subtle but visible
  return { className: 'text-orange-600 font-semibold', title };
}

function renderAlertCell<T>(
  value: T,
  row: any,
  columnKey: string,
  lang: string,
  alertsList: { Id: string; En: string; Fr: string; CampaignProperty: string }[],
  formatter: (v: T, row?: any) => string | number | JSX.Element
) {
  const { className, title } = getAlertClassAndTitle(
    alertsList,
    columnKey,
    row?.original?.Alerts,
    lang
  );

  return (
    <span className={className} title={title}>
      {formatter(value, row)}
    </span>
  );
}

export const useCampaignsTable = (
  filters: CampaignDashboardFilter
): ColumnDef<Campaign>[] => {
  const { t } = useTranslation('campaign');
  const lang = useLanguageStore((s) => s.language);
  console.log('useCampaignsTable filters:', filters);
  const sortableHeader = (label: string, column: any) => {
    const sorted = column.getIsSorted(); // "asc" | "desc" | false

    return (
      <Button
        variant="ghost"
        className="px-1 font-medium"
        onClick={() => {
          const nextSort =
            column.getIsSorted() === 'asc'
              ? 'desc'
              : column.getIsSorted() === 'desc'
                ? null // removes sorting
                : 'asc';
          column.toggleSorting(nextSort);
        }}
      >
        {label}
        {sorted === 'asc' ? (
          <ArrowUp className="ml-1 h-4 w-4 text-foreground" />
        ) : sorted === 'desc' ? (
          <ArrowDown className="ml-1 h-4 w-4 text-foreground" />
        ) : (
          <ArrowUpDown className="ml-1 h-4 w-4 text-muted-foreground" />
        )}
      </Button>
    );
  };

  // helper to attach alert-aware cell with optional row-aware formatter
  const makeCell = (
    columnKey: string,
    formatter: (v: any, row?: any) => any = (v) => v
  ) => ({
    cell: ({ getValue, row }: any) =>
      renderAlertCell(
        getValue(),
        row,
        columnKey,
        lang,
        filters.Alerts,
        formatter
      )
  });
  
  const baseColumns: ColumnDef<Campaign>[] = [
    // ---------------- General Information ----------------
    {
      id: 'general',
      header: () => t('dashboard.groups.general'),
      enableColumnFilter: false,
      columns: [
        {
          accessorKey: 'Id',
          enableColumnFilter: false,
          header: ({ column }) =>
            sortableHeader(t('dashboard.columns.id'), column),
          ...makeCell('Id', (value, row) => {
            const id = value as number;
            return (
              <Link
                to={`/modif_campagne.asp?idCampaign=${id}`}
                className="text-blue-600 hover:underline"
              >
                {id}
              </Link>
            );
          })
        },
        {
          accessorKey: 'SurveyCode',
          size: 300,
          minSize: 300,
          enableColumnFilter: false,
          header: ({ column }) => (
            <div className="w-[300px]">
              {sortableHeader(t('dashboard.columns.surveyCode'), column)}
            </div>
          ),
          ...makeCell('SurveyCode', (value, row) => {
            const survey = value as string;
            const client = row?.original?.ClientName;
            const icost = row?.original?.IcostCode;
            const unit4 = row?.original?.Unit4ProjectId;
            return (
              <div className="w-[300px] flex flex-col text-wrap">
                <span className="font-medium">{survey}</span>
                {client && (
                  <span className="text-xs text-muted-foreground">
                    {client}
                    {icost && ` (Icost: ${icost})`}
                    {unit4 && !icost && ` (Unit4: ${unit4})`}
                  </span>
                )}
              </div>
            );
          })
        },
        {
          accessorKey: 'StartDate',
          enableColumnFilter: false,
          header: ({ column }) =>
            sortableHeader(t('dashboard.columns.startDate'), column),
          ...makeCell('StartDate', (value) => formatDate(value as string))
        },
        {
          accessorKey: 'EndDate',
          enableColumnFilter: false,
          header: ({ column }) =>
            sortableHeader(t('dashboard.columns.endDate'), column),
          ...makeCell('EndDate', (value) => formatDate(value as string))
        },
        {
          accessorKey: 'CampaignType',
          header: ({ column }) =>
            sortableHeader(t('dashboard.columns.campaignType'), column),
          enableColumnFilter: true,
          ...makeCell('CampaignType', (value, row) => {
            const campaignTypeId = value?.toString();
            const match = filters.CampaignTypes.find(
              (type) => type.CampaignTypeId === campaignTypeId
            );
            if (!match) return '—';
            return lang === 'fr' ? match.Fr : match.En;
          }),
          meta: {
            filterVariant: 'select',
            filterOptions: filters.CampaignTypes.map((type) => ({
              label: lang === 'fr' ? type.Fr : type.En,
              value: type.CampaignTypeId
            }))
          }
        },
        {
          accessorKey: 'PmName',
          header: ({ column }) =>
            sortableHeader(t('dashboard.columns.pm'), column),
          enableColumnFilter: true,
          ...makeCell('PmName'),
          meta: {
            filterVariant: 'select',
            filterOptions: filters.PmNames.map((name) => ({
              label: name,
              value: name
            }))
          }
        },
        {
          accessorKey: 'SubPanel',
          header: ({ column }) =>
            sortableHeader(t('dashboard.columns.subpanel'), column),
          enableColumnFilter: true,
          ...makeCell('SubPanel'),
          meta: {
            filterVariant: 'select',
            filterOptions: filters.SubPanels.map((sp) => ({
              label: sp,
              value: sp
            }))
          }
        },
        {
          accessorKey: 'SiteStatus',
          header: ({ column }) =>
            sortableHeader(t('dashboard.columns.siteStatus'), column),
          enableColumnFilter: true,
          ...makeCell('SiteStatus', (value) => (
            <Badge variant="outline" className="capitalize">
              {value as string}
            </Badge>
          )),
          meta: {
            filterVariant: 'select',
            filterOptions: filters.SiteStatuses.map((status) => ({
              label: status,
              value: status
            }))
          }
        },
        {
          accessorKey: 'EmailStatus',
          header: ({ column }) =>
            sortableHeader(t('dashboard.columns.emailStatus'), column),
          enableColumnFilter: true,
          ...makeCell('EmailStatus', (value) => (
            <Badge variant="outline" className="capitalize">
              {value as string}
            </Badge>
          )),
          meta: {
            filterVariant: 'select',
            filterOptions: filters.EmailStatuses.map((status) => ({
              label: status,
              value: status
            }))
          }
        }
      ]
    },

    // ---------------- Completes & Quotas ----------------
    {
      id: 'completes',
      header: () => t('dashboard.groups.completes'),
      columns: [
        {
          accessorKey: 'TargetedCompletes',
          enableColumnFilter: false,
          header: ({ column }) =>
            sortableHeader(t('dashboard.columns.targetedCompletes'), column),
          ...makeCell('TargetedCompletes', (value) => formatNumber(value as number))
        },
        {
          accessorKey: 'ActualCompletes',
          enableColumnFilter: false,
          header: ({ column }) =>
            sortableHeader(t('dashboard.columns.actualCompletes'), column),
          ...makeCell('ActualCompletes', (value) => formatNumber(value as number))
        },
        {
          accessorKey: 'ActualScreened',
          enableColumnFilter: false,
          header: ({ column }) =>
            sortableHeader(t('dashboard.columns.actualScreened'), column),
          ...makeCell('ActualScreened', (value) => formatNumber(value as number))
        },
        {
          accessorKey: 'ActualQuotas',
          enableColumnFilter: false,
          header: ({ column }) =>
            sortableHeader(t('dashboard.columns.actualQuotas'), column),
          ...makeCell('ActualQuotas', (value) => formatNumber(value as number))
        },
        {
          accessorKey: 'ActualIncompletes',
          enableColumnFilter: false,
          header: ({ column }) =>
            sortableHeader(t('dashboard.columns.actualIncompletes'), column),
          ...makeCell('ActualIncompletes', (value) => formatNumber(value as number))
        },
        {
          accessorKey: 'ActualTrapped',
          enableColumnFilter: false,
          header: ({ column }) =>
            sortableHeader(t('dashboard.columns.actualTrapped'), column),
          ...makeCell('ActualTrapped', (value) => formatNumber(value as number))
        },
        {
          accessorKey: 'ActualFailed',
          enableColumnFilter: false,
          header: ({ column }) =>
            sortableHeader(t('dashboard.columns.actualFailed'), column),
          ...makeCell('ActualFailed', (value) => formatNumber(value as number))
        },
        {
          accessorKey: 'ActualIPFiltered',
          enableColumnFilter: false,
          header: ({ column }) =>
            sortableHeader(t('dashboard.columns.actualIpFiltered'), column),
          ...makeCell('ActualIPFiltered', (value) => formatNumber(value as number))
        }
      ]
    },

    // ---------------- Incidence & Duration ----------------
    {
      id: 'incidence',
      header: () => t('dashboard.groups.incidence'),
      columns: [
        {
          accessorKey: 'TargetedIncidence',
          enableColumnFilter: false,
          header: ({ column }) =>
            sortableHeader(t('dashboard.columns.targetedIncidence'), column),
          ...makeCell('TargetedIncidence', (value) => formatPercent(value as number))
        },
        {
          accessorKey: 'ActualIncidence',
          enableColumnFilter: false,
          header: ({ column }) =>
            sortableHeader(t('dashboard.columns.actualIncidence'), column),
          ...makeCell('ActualIncidence', (value) => formatPercent(value as number))
        },
        {
          accessorKey: 'TargetedDuration',
          enableColumnFilter: false,
          header: ({ column }) =>
            sortableHeader(t('dashboard.columns.targetedDuration'), column),
          ...makeCell('TargetedDuration', (value) => formatNumber(value as number))
        },
        {
          accessorKey: 'MedianDuration',
          enableColumnFilter: false,
          header: ({ column }) =>
            sortableHeader(t('dashboard.columns.medianDuration'), column),
          ...makeCell('MedianDuration', (value) => (value as string) ?? '—')
        },
        {
          accessorKey: 'MedianScreener',
          enableColumnFilter: false,
          header: ({ column }) =>
            sortableHeader(t('dashboard.columns.medianScreener'), column),
          ...makeCell('MedianScreener', (value) => (value as string) ?? '—')
        }
      ]
    },

    // ---------------- Rates & Incentives ----------------
    {
      id: 'rates',
      header: () => t('dashboard.groups.rates'),
      columns: [
        {
          accessorKey: 'Incentive',
          enableColumnFilter: false,
          header: ({ column }) =>
            sortableHeader(t('dashboard.columns.incentive'), column),
          ...makeCell('Incentive', (value) => formatNumber(value as number))
        },
        {
          accessorKey: 'ActualAnswerRate',
          enableColumnFilter: false,
          header: ({ column }) =>
            sortableHeader(t('dashboard.columns.actualAnswerRate'), column),
          ...makeCell('ActualAnswerRate', (value) => formatPercent(value as number))
        },
        {
          accessorKey: 'ActualIncompletionRate',
          enableColumnFilter: false,
          header: ({ column }) =>
            sortableHeader(
              t('dashboard.columns.actualIncompletionRate'),
              column
            ),
          ...makeCell('ActualIncompletionRate', (value) => formatPercent(value as number))
        }
      ]
    },

    // ---------------- Portal Stats ----------------
    {
      id: 'portal',
      header: () => t('dashboard.groups.portal'),
      columns: [
        {
          accessorKey: 'PortalCompletes',
          enableColumnFilter: false,
          header: ({ column }) =>
            sortableHeader(t('dashboard.columns.portalCompletes'), column),
          ...makeCell('PortalCompletes', (value) => formatNumber(value as number))
        },
        {
          accessorKey: 'PortalScreened',
          enableColumnFilter: false,
          header: ({ column }) =>
            sortableHeader(t('dashboard.columns.portalScreened'), column),
          ...makeCell('PortalScreened', (value) => formatNumber(value as number))
        },
        {
          accessorKey: 'PortalQuotas',
          enableColumnFilter: false,
          header: ({ column }) =>
            sortableHeader(t('dashboard.columns.portalQuotas'), column),
          ...makeCell('PortalQuotas', (value) => formatNumber(value as number))
        },
        {
          accessorKey: 'PortalIncompletes',
          enableColumnFilter: false,
          header: ({ column }) =>
            sortableHeader(t('dashboard.columns.portalIncompletes'), column),
          ...makeCell('PortalIncompletes', (value) => formatNumber(value as number))
        },
        {
          accessorKey: 'PortalIncidence',
          enableColumnFilter: false,
          header: ({ column }) =>
            sortableHeader(t('dashboard.columns.portalIncidence'), column),
          ...makeCell('PortalIncidence', (value) => formatPercent(value as number))
        },
        {
          accessorKey: 'PortalAnswerRate',
          enableColumnFilter: false,
          header: ({ column }) =>
            sortableHeader(t('dashboard.columns.portalAnswerRate'), column),
          ...makeCell('PortalAnswerRate', (value) => formatPercent(value as number))
        },
        {
          accessorKey: 'PortalIncompletionRate',
          enableColumnFilter: false,
          header: ({ column }) =>
            sortableHeader(
              t('dashboard.columns.portalIncompletionRate'),
              column
            ),
          ...makeCell('PortalIncompletionRate', (value) => formatPercent(value as number))
        }
      ]
    },

    // ---------------- Emails ----------------
    {
      id: 'emails',
      header: () => t('dashboard.groups.emails'),
      columns: [
        {
          accessorKey: 'EmailsSent',
          enableColumnFilter: false,
          header: ({ column }) =>
            sortableHeader(t('dashboard.columns.emailsSent'), column),
          ...makeCell('EmailsSent', (value) => formatNumber(value as number))
        },
        {
          accessorKey: 'EmailsCompletes',
          enableColumnFilter: false,
          header: ({ column }) =>
            sortableHeader(t('dashboard.columns.emailsCompletes'), column),
          ...makeCell('EmailsCompletes', (value) => formatNumber(value as number))
        },
        {
          accessorKey: 'EmailsScreened',
          enableColumnFilter: false,
          header: ({ column }) =>
            sortableHeader(t('dashboard.columns.emailsScreened'), column),
          ...makeCell('EmailsScreened', (value) => formatNumber(value as number))
        },
        {
          accessorKey: 'EmailsQuotas',
          enableColumnFilter: false,
          header: ({ column }) =>
            sortableHeader(t('dashboard.columns.emailsQuotas'), column),
          ...makeCell('EmailsQuotas', (value) => formatNumber(value as number))
        },
        {
          accessorKey: 'EmailsIncompletes',
          enableColumnFilter: false,
          header: ({ column }) =>
            sortableHeader(t('dashboard.columns.emailsIncompletes'), column),
          ...makeCell('EmailsIncompletes', (value) => formatNumber(value as number))
        },
        {
          accessorKey: 'EmailsClosed',
          enableColumnFilter: false,
          header: ({ column }) =>
            sortableHeader(t('dashboard.columns.emailsClosed'), column),
          ...makeCell('EmailsClosed', (value) => formatNumber(value as number))
        },
        {
          accessorKey: 'EmailsPaused',
          enableColumnFilter: false,
          header: ({ column }) =>
            sortableHeader(t('dashboard.columns.emailsPaused'), column),
          ...makeCell('EmailsPaused', (value) => formatNumber(value as number))
        },
        {
          accessorKey: 'EmailsIncidence',
          enableColumnFilter: false,
          header: ({ column }) =>
            sortableHeader(t('dashboard.columns.emailsIncidence'), column),
          ...makeCell('EmailsIncidence', (value) => formatPercent(value as number))
        },
        {
          accessorKey: 'EmailsAnswerRate',
          enableColumnFilter: false,
          header: ({ column }) =>
            sortableHeader(t('dashboard.columns.emailsAnswerRate'), column),
          ...makeCell('EmailsAnswerRate', (value) => formatPercent(value as number))
        },
        {
          accessorKey: 'EmailsIncompletionRate',
          enableColumnFilter: false,
          header: ({ column }) =>
            sortableHeader(
              t('dashboard.columns.emailsIncompletionRate'),
              column
            ),
          ...makeCell('EmailsIncompletionRate', (value) => formatPercent(value as number))
        }
      ]
    },

    // ---------------- Reminders ----------------
    {
      id: 'reminders',
      header: () => t('dashboard.groups.reminders'),
      columns: [
        {
          accessorKey: 'RemindersSent',
          enableColumnFilter: false,
          header: ({ column }) =>
            sortableHeader(t('dashboard.columns.remindersSent'), column),
          ...makeCell('RemindersSent', (value) => formatNumber(value as number))
        },
        {
          accessorKey: 'RemindersCompletes',
          enableColumnFilter: false,
          header: ({ column }) =>
            sortableHeader(t('dashboard.columns.remindersCompletes'), column),
          ...makeCell('RemindersCompletes', (value) => formatNumber(value as number))
        },
        {
          accessorKey: 'RemindersScreened',
          enableColumnFilter: false,
          header: ({ column }) =>
            sortableHeader(t('dashboard.columns.remindersScreened'), column),
          ...makeCell('RemindersScreened', (value) => formatNumber(value as number))
        },
        {
          accessorKey: 'RemindersQuotas',
          enableColumnFilter: false,
          header: ({ column }) =>
            sortableHeader(t('dashboard.columns.remindersQuotas'), column),
          ...makeCell('RemindersQuotas', (value) => formatNumber(value as number))
        },
        {
          accessorKey: 'RemindersIncompletes',
          enableColumnFilter: false,
          header: ({ column }) =>
            sortableHeader(t('dashboard.columns.remindersIncompletes'), column),
          ...makeCell('RemindersIncompletes', (value) => formatNumber(value as number))
        },
        {
          accessorKey: 'RemindersClosed',
          enableColumnFilter: false,
          header: ({ column }) =>
            sortableHeader(t('dashboard.columns.remindersClosed'), column),
          ...makeCell('RemindersClosed', (value) => formatNumber(value as number))
        },
        {
          accessorKey: 'RemindersPaused',
          enableColumnFilter: false,
          header: ({ column }) =>
            sortableHeader(t('dashboard.columns.remindersPaused'), column),
          ...makeCell('RemindersPaused', (value) => formatNumber(value as number))
        },
        {
          accessorKey: 'RemindersIncidence',
          enableColumnFilter: false,
          header: ({ column }) =>
            sortableHeader(t('dashboard.columns.remindersIncidence'), column),
          ...makeCell('RemindersIncidence', (value) => formatPercent(value as number))
        },
        {
          accessorKey: 'RemindersAnswerRate',
          enableColumnFilter: false,
          header: ({ column }) =>
            sortableHeader(t('dashboard.columns.remindersAnswerRate'), column),
          ...makeCell('RemindersAnswerRate', (value) => formatPercent(value as number))
        },
        {
          accessorKey: 'RemindersIncompletionRate',
          enableColumnFilter: false,
          header: ({ column }) =>
            sortableHeader(
              t('dashboard.columns.remindersIncompletionRate'),
              column
            ),
          ...makeCell('RemindersIncompletionRate', (value) => formatPercent(value as number))
        }
      ]
    },

    // ---------------- Alerts & Other ----------------
    {
      id: 'alerts',
      header: () => t('dashboard.groups.alerts'),
      columns: [
        {
          accessorKey: 'NumberOfAlerts',
          enableColumnFilter: false,
          header: ({ column }) =>
            sortableHeader(t('dashboard.columns.numberOfAlerts'), column),
          ...makeCell('NumberOfAlerts', (value) => formatNumber(value as number))
        },
        {
          accessorKey: 'ActualTickets',
          enableColumnFilter: false,
          header: ({ column }) =>
            sortableHeader(t('dashboard.columns.actualTickets'), column),
          ...makeCell('ActualTickets', (value, row) => (
            <Link
              to={`/billetsCampaign.asp?idcampaign=${row?.original?.Id}`}
              className="text-blue-600 hover:underline"
            >
              {formatNumber(value as number)}
            </Link>
          ))
        },
        {
          accessorKey: 'MaxReminders',
          enableColumnFilter: false,
          header: ({ column }) =>
            sortableHeader(t('dashboard.columns.maxReminders'), column),
          ...makeCell('MaxReminders', (value) => formatNumber(value as number))
        }
      ]
    }
  ];

  return useMemo(() => baseColumns, [t, filters]);
};
