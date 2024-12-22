import { ApiResponse } from "@/types/api/api-response";
import { api } from "@/lib/api-client";
import { API_PATHS } from "@/configurations/api-paths";
import { FacebookPageData } from "@/types/facebook/facebook-page-data";

export const getFacebookPageData = async (accessToken: string, pageId: string): Promise<ApiResponse<FacebookPageData>> => {
    const response = await api.call<FacebookPageData>(API_PATHS.facebook.pageData(pageId), {
        headers: {
            Authorization: `Bearer ${accessToken}`
        }
    });

    return response;
};