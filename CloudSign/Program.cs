HostFactory.Run(windowsService =>
{
    windowsService.Service<SystemService>(se =>
    {
        se.ConstructUsing(n =>
            {
                ST.SetConfigFolderName(n.InstanceName);

                return new SystemService();
            }
        );
        se.WhenStarted(wr => wr.Start(args));
        se.WhenStopped(wr => wr.Stop());
    });
    windowsService.RunAsLocalSystem();

    windowsService.SetServiceName("CloudSignService");
    windowsService.SetDescription("CloudSignService");
    windowsService.SetDisplayName("CloudSignService");
});