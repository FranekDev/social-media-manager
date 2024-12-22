
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs";
import { Skeleton } from "@/components/ui/skeleton";

type TabsContentProps = {
    tabs: TabContent[];
    isLoading: boolean;
};

export default function TabsView({ tabs, isLoading }: TabsContentProps) {


    return (isLoading ? <Skeleton className="w-full h-96"/> :
        <Tabs defaultValue={tabs.length > 0 ? tabs[0].value : undefined}>
            <TabsList>
                {tabs.map((tab) => (
                    <TabsTrigger key={tab.value} value={tab.value}>
                        {tab.title}
                    </TabsTrigger>
                ))}
            </TabsList>
            {tabs.map((tab) => (
                <TabsContent key={tab.value} value={tab.value}>
                    {tab.content}
                </TabsContent>
            ))}
        </Tabs>
    );
}