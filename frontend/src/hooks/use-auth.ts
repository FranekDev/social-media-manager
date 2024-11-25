import { useSession } from "next-auth/react";
import { useEffect, useState } from "react";
import { usePathname, useRouter } from "next/navigation";

type User = {
    username: string;
    email: string;
};

export function useAuth() {
    const { data: session } = useSession();
    const router = useRouter();
    const pathname = usePathname();

    const [user, setUser] = useState<User | null>(null);

    useEffect(() => {
        if (!session && pathname !== "/login") {
            setUser(null);
            router.replace("/login");
        } else if (session) {
            setUser({
                username: session?.user.token.username as string,
                email: session?.user.token.email as string
            } as User | null);
        }
    }, [session, router, pathname]);

    return {
        token: session?.user.token?.token as string,
        user: user,
        isAuthenticated: !!session
    };
}