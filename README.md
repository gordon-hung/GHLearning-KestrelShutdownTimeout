# GHLearning-KestrelShutdownTimeout

### 關機逾時
指定等待 Web 主機關機的時間長度。

- 索引鍵：shutdownTimeoutSeconds
- 類型：int
- 預設值：5
- 設定使用：UseShutdownTimeout
- 環境變數：ASPNETCORE_SHUTDOWNTIMEOUTSECONDS

雖然索引鍵接受 int 與 UseSetting (例如 .UseSetting(WebHostDefaults.ShutdownTimeoutKey, "10"))，UseShutdownTimeout 擴充方法會採用 TimeSpan。

#### 在逾時期間，裝載會：
- 觸發程序 IApplicationLifetime.ApplicationStopping。
- 嘗試停止託管的服務，並記錄無法停止之服務的任何錯誤。

#### [UseShutdownTimeout Default](https://source.dot.net/#Microsoft.Extensions.Hosting/HostOptions.cs)
```
        /// <summary>
        /// Gets or sets the default timeout for <see cref="IHost.StopAsync(CancellationToken)"/>.
        /// </summary>
        /// <remarks>
        /// This timeout also encompasses all host services implementing
        /// <see cref="IHostedLifecycleService.StoppingAsync(CancellationToken)"/> and
        /// <see cref="IHostedLifecycleService.StoppedAsync(CancellationToken)"/>.
        /// </remarks>
        public TimeSpan ShutdownTimeout { get; set; } = TimeSpan.FromSeconds(30);
```

如果在所有的託管服務停止之前逾時期限已到期，則應用程式關閉時，會停止任何剩餘的作用中服務。 即使服務尚未完成處理也會停止。 如果服務需要更多時間才能停止，請增加逾時。

### ASPNETCORE_SHUTDOWNTIMEOUTSECONDS
```
"ASPNETCORE_SHUTDOWNTIMEOUTSECONDS": "40"
```

### UseShutdownTimeout
```
builder.WebHost.UseShutdownTimeout()
```

## 資料來源
- [C# 關機逾時](https://learn.microsoft.com/zh-tw/aspnet/core/fundamentals/host/web-host?view=aspnetcore-9.0#shutdown-timeout)
- [HostOptions](https://source.dot.net/#Microsoft.Extensions.Hosting/HostOptions.cs)