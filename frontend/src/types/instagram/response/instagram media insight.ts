interface InsightValue {
    value: number;
}

export interface InsightDataItem {
    name: string;
    period: string;
    values: InsightValue[];
    title: string;
    description: string;
    id: string;
}

export interface InstagramInsightsResponse {
    data: InsightDataItem[];
}