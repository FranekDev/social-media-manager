import { API_PATHS } from "@/configurations/api-paths";
import { api } from "@/lib/api-client";
import { ApiResponse } from "@/types/api/api-response";
import { TikTokPhotoRequest } from "@/types/tiktok/request/tiktok-photo-request";
import { TikTokSchedulePhotoResponse } from "@/types/tiktok/response/tiktok-schedule-photo-response";

export const scheduleTiktokPhoto = async (accessToken: string, body: TikTokPhotoRequest): Promise<ApiResponse<TikTokSchedulePhotoResponse>> => {
    const response = await api.call<TikTokSchedulePhotoResponse>(API_PATHS.tiktok.schedulePhoto, {
        body,
        headers: {
            Authorization: `Bearer ${accessToken}`
        }
    });

    return response;
}