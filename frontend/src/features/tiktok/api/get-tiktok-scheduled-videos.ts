import { API_PATHS } from "@/configurations/api-paths";
import { api } from "@/lib/api-client";
import { ApiResponse } from "@/types/api/api-response";
import { TikTokVideo } from "@/types/tiktok/response/tiktok-video";

export const getTiktokScheduledVideos = async (accessToken: string): Promise<ApiResponse<TikTokVideo[]>> => {
    const response = await api.call<TikTokVideo[]>(API_PATHS.tiktok.getSchduledVideos, {
        headers: {
            Authorization: `Bearer ${accessToken}`
        }
    });

    return response;
}