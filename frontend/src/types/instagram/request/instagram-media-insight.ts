import { InstagramMediaInsightType } from "@/types/instagram/enums/instagram-media-insight-type";

export type InstagramMediaInsightRequest = {
    mediaId: string;
    insightType: InstagramMediaInsightType;
};