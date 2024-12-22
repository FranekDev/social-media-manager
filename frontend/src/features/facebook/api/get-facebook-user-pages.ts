import { ApiResponse } from "@/types/api/api-response";
import { FacebookUserPage } from "@/types/facebook/facebook-user-page";
import { api } from "@/lib/api-client";
import { API_PATHS } from "@/configurations/api-paths";

export const getFacebookUserPages = async (accessToken: string): Promise<ApiResponse<FacebookUserPage[]>> => {
    const response = await api.call<FacebookUserPage[]>(API_PATHS.facebook.getUserPages, {
        headers: {
            Authorization: `Bearer ${accessToken}`
        }
    });

    return response;
};