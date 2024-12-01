import { api } from "@/lib/api-client";
import { API_PATHS } from "@/configurations/api-paths";
import { InstagramUserDetail } from "@/types/instagram/response/instagram-user-detail";
import { ApiResponse } from "@/types/api/api-response";

export const getInstagramUser = async (accessToken: string): Promise<ApiResponse<InstagramUserDetail>> => {
    const response = await api.call<InstagramUserDetail>(API_PATHS.instagram.getUser, {
        headers: {
            Authorization: `Bearer ${accessToken}`
        }
    });

    return response;
}