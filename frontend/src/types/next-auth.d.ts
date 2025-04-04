﻿// src/types/next-auth.d.ts
import { DefaultSession } from "next-auth";
import { JWT } from "next-auth/jwt";

declare module "next-auth" {
    interface Session {
        user: {
            token: JWT;
        } & DefaultSession["user"];
    }
}