﻿import { API_PATHS } from "@/configurations/api-paths";
import { api } from "@/lib/api-client";
import { InstagramSchedulePostResponse } from "@/types/instagram/response/instagram-api-response";
import { InstagramSchedulePostRequest } from "@/types/instagram/request/instagram-schedule-post";
import { ApiResponse } from "@/types/api/api-response";

export const scheduleInstagramPost = async (accessToken: string, body: InstagramSchedulePostRequest)
    : Promise<ApiResponse<InstagramSchedulePostResponse>> => {
    const response = await api.call<InstagramSchedulePostResponse>(API_PATHS.instagram.schedulePost, {
        body,
        headers: {
            Authorization: `Bearer ${accessToken}`
        }
    });

    return response;
};


