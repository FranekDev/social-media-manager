import type {
    GetServerSidePropsContext,
    NextApiRequest,
    NextApiResponse,
} from "next";
import type { NextAuthOptions } from "next-auth";
import { getServerSession } from "next-auth";
import CredentialsProvider from "next-auth/providers/credentials";

export const config = {
    session: {
        strategy: "jwt",
    },
    providers: [
        CredentialsProvider({
            name: "credentials",
            credentials: {
                username: {},
                password: {},
            },
            async authorize(credentials) {
                const response = await fetch("https://localhost:7114/api/account/login", {
                    method: "POST",
                    headers: {
                        Accept: "application/json",
                        "Content-Type": "application/json ",
                    },
                    body: JSON.stringify({
                        username: credentials?.username,
                        password: credentials?.password,
                    }),
                });
                const data = await response.json();

                if (response.ok && data) {
                    return data;
                }
                return null;
            },
        }),
    ],
    callbacks: {
        async jwt({ token, user }) {
            return { ...token, ...user };
        },
        async session({ session, token }) {
            session.user.token = token;
            return session;
        },
    },
    secret: process.env.NEXTAUTH_SECRET,
    pages: {
        signIn: "/login",
    },
} satisfies NextAuthOptions;

export function auth(
    ...args:
        | [GetServerSidePropsContext["req"], GetServerSidePropsContext["res"]]
        | [NextApiRequest, NextApiResponse]
        | []
) {
    return getServerSession(...args, config);
}