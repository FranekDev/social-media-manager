import { API_PATHS } from "@/configurations/api-paths";
import { api } from "@/lib/api-client";
import { ApiResponse } from "@/types/api/api-response";
import { TikTokVideoRequest } from "@/types/tiktok/request/tiktok-video-request";
import { TikTokScheduleVideoResponse } from "@/types/tiktok/response/tiktok-schedule-video-response";

export const scheduleTiktokVideo = async (accessToken: string, body: TikTokVideoRequest): Promise<ApiResponse<TikTokScheduleVideoResponse>> => {
    const response = await api.call<TikTokScheduleVideoResponse>(API_PATHS.tiktok.scheduleVideo, {
        body,
        headers: {
            Authorization: `Bearer ${accessToken}`
        }
    });

    return response;
}