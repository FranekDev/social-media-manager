import { api } from "@/lib/api-client";
import { API_PATHS } from "@/configurations/api-paths";
import { ApiResponse } from "@/types/api/api-response";
import { InstagramReel } from "@/types/instagram/response/instagram-reel";

export const getInstagramScheduledReels = async (accessToken: string): Promise<ApiResponse<InstagramReel[]>> => {
    const response = await api.call<InstagramReel[]>(API_PATHS.instagram.getScheduledReels, {
        headers: {
            Authorization: `Bearer ${accessToken}`
        }
    });

    return response;
}