import { API_PATHS } from "@/configurations/api-paths";
import { api } from "@/lib/api-client";
import { ApiResponse } from "@/types/api/api-response";
import { TikTokUserResponse } from "@/types/tiktok/response/tiktok-user";

export const getTiktokUserInfo = async (accessToken: string): Promise<ApiResponse<TikTokUserResponse>> => {
    const response = await api.call<TikTokUserResponse>(API_PATHS.tiktok.getUser, {
        headers: {
            Authorization: `Bearer ${accessToken}`
        }
    });

    return response;
}