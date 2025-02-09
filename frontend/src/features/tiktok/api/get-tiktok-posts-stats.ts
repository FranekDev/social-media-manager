import { API_PATHS } from "@/configurations/api-paths";
import { api } from "@/lib/api-client";
import { ApiResponse } from "@/types/api/api-response";
import { TikTokVideoInfo } from "@/types/tiktok/response/tiktok-video-info";

export const getTiktokPostsStats = async (accessToken: string): Promise<ApiResponse<TikTokVideoInfo[]>> => {
    const response = await api.call<TikTokVideoInfo[]>(API_PATHS.tiktok.getPostsStats, {
        headers: {
            Authorization: `Bearer ${accessToken}`
        }
    });

    return response;
}