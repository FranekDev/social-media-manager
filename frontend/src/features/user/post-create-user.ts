import { API_PATHS } from "@/configurations/api-paths";

export type Register = {
    username: string;
    email: string;
    password: string;
};

export type NewUser = {
    username: string;
    email: string;
    token: string;
};

export const registerUser = async (body: Register): Promise<NewUser> => {
    const response = await fetch(API_PATHS.user.register.url, {
        method: API_PATHS.user.register.method,
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(body),
    });

    if (!response.ok) {
        throw new Error("Failed to register user");
    }

    return response.json();
}