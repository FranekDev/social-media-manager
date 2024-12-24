import { API_PATHS } from "@/configurations/api-paths";
import { api } from "@/lib/api-client";
import { ApiResponse } from "@/types/api/api-response";
import { TikTokPhoto } from "@/types/tiktok/response/tiktok-photo";

export const getTiktokScheduledPhotos = async (accessToken: string): Promise<ApiResponse<TikTokPhoto[]>> => {
    const response = await api.call<TikTokPhoto[]>(API_PATHS.tiktok.getScheduledPhotos, {
        headers: {
            Authorization: `Bearer ${accessToken}`
        }
    });

    return response;
}