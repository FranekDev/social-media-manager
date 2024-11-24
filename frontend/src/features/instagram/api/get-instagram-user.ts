import { api } from "@/lib/api-client";
import { API_PATHS } from "@/configurations/api-paths";
import { InstagramUserDetail } from "@/types/instagram/response/instagram-user-detail";

export const getInstagramUser = async (accessToken: string): Promise<InstagramUserDetail> => {
    const response = await api.call<InstagramUserDetail>(API_PATHS.instagram.getUser, {
        headers: {
            Authorization: `Bearer ${accessToken}`
        }
    });

    return response;
}