# QuartzNet 介紹 #
---
- 三大元件
    - Scheduler
        - 排成器
    - Job
        - 執行動作
    - Trigger  
        - 觸發器 
       

-  簡單範例
    1. 建立Scheduler
        ```csharp
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();`
        ```
    2. 建立Job
        ```csharp
            var job = JobBuilder.Create<HelloJob>().Build();
        ```
    3. Job內容需實作IJob

        ```csharp
            class HelloJob : IJob
            {
                void IJob.Execute(IJobExecutionContext context)
                {
                    Console.WriteLine("Hellow JOB");
                }
            }
        ```

    4. 建立Trigger

         ```csharp
            var trigger = TriggerBuilder.Create()
                .WithSimpleSchedule(x =>
                x.WithIntervalInSeconds(3).RepeatForever())
                .Build(); //每三秒執行一次
        ```
        
    
    5. 註冊Job以及Trigger到Scheduler
      
        ```csharp
            var scheduler.ScheduleJob(job, trigger); //註冊Job 以及Trigger 到 Scheduler
        ```
    
    6. 開啟Scheduler 
        ```csharp
            scheduler.Start();
        ```
- Job 其他註冊方式
    - 給予Job名稱
    - 在scheduler搭配trigger註冊
    ```csharp
        var job1 = JobBuilder.Create<HelloJob>().WithIdentity("Job1")
        var trigger = TriggerBuilder.Create().StartNow().ForJob("Job1").Build();
        await scheduler.AddJob(job1, true);
        await scheduler.ScheduleJob(trigger);
    ```


- Trigger週期
    - 每天某一個時間點
        ```csharp
            var dinnerTime = DateBuilder.TodayAt(14, 33, 50);//每天14點33分50秒是晚餐時間
            var trigger = TriggerBuilder.Create()
                .WithSimpleSchedule().StartAt(dinnerTime).Build();
        ```
    - 有週期性
        - 使用Cron-Experssions 
        ```csharp
        var trigger = TriggerBuilder.Create() //每週四15點12分30秒Tigger
                .WithCronSchedule("30 12 15 ? * THU").Build();
        ```

- Listener
    - 建立Listener
    ```csharp
        public class MyJobListener :IJobListener
        {
            public Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = new CancellationToken())
            {
                //執行前
                Console.WriteLine("before");
                return Task.Delay(0);
            }

            public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = new CancellationToken())
            {
                
                return Task.Delay(0);
            }

            public Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException,
                CancellationToken cancellationToken = new CancellationToken())
            {
                //執行前
                Console.WriteLine("after");
                return Task.Delay(0);
            }
        }
    ```
      
    - 註冊Listener
        - Listener可以設定Job在執行前後自訂想要的型別

    ```csharp       
        scheduler.ListenerManager.AddJobListener(
                  new MyJobListener(), KeyMatcher<JobKey>.KeyEquals(new JobKey("Job1")));
    ```

    
    - 將設定放入DB
        - 使用MySDL當作範例
        - 首先建立DB的schema
            - https://github.com/quartznet/quartznet/blob/master/database/tables/tables_mysql_innodb.sql github上面有很多不同DB的schema
        -  ![DBSchema](https://github.com/otot333/QuartznetLab/blob/master/Dbschema.png)
     

```mermaid
graph LR;
A(HR面試) ==> B(選擇題紙本考試);
B == 超過65分 ==> C(技術面試);
C == 如果是Senior position ==> D(例題作業測驗);
```
    





---
---

---
---
---
*abc*
**abc**
***abc***
####
----
[GoToGoogle](http://www.google.com.tw, "我是谷歌")
[here][3]
[3]: http://www.google.com.tw GotoGoogle again

[here][3]
然后在别的地方定义 3 这个详细链接信息，
[3]: http://www.izhangbo.cn "聚牛团队"

<kbd>Ctrl+A</kbd> and <kbd>Ctrl+B</kbd>

Use the `printf()` function

``There is a literal backtick (`) here.针对在代码区段内插入反引号的情况`` 

强调：
*斜体强调*
**粗体强调**

 图片
![Alt text](http://www.izhangbo.cn/wp-content/themes/minty/img/logo.png "Optional title")

使用 icon 图标文字
<i class="icon-cog"></i>

Item         | Value
------------ | ---
Computer     | $1600
Phone        | $12
Pipe         | $1

- 無需列表１
- 無序列表２

1. 使用列表
2. 使用列表
3. 使用列表
4. 使用列表
5. 使用列表

> 在非洲每六十秒

我是第一段

我是第二段  

我是第三行

下午茶
* 雞排
+ 珍珠奶茶
- 甜不辣


下午茶2

scheduler.ScheduleJob(job, trigger);

- 參考來源


1. https://www.quartz-scheduler.net/documentation/quartz-2.x/tutorial/crontriggers.html

2. https://hk.saowen.com/a/f9537f731b4b8d2d7dd24f6f769f9ffb7a4199a98d0841750ba7639366c72bf6

3. https://www.quartz-scheduler.net/documentation/quartz-3.x/tutorial/jobs-and-triggers.html

4. https://www.quartz-scheduler.net/documentation/quartz-3.x/tutorial/crontriggers.html

5. https://www.quartz-scheduler.net/documentation/quartz-3.x/tutorial/crontrigger.html

6. https://www.freeformatter.com/cron-expression-generator-quartz.html
7. https://www.cnblogs.com/yaopengfei/p/8577934.html
8. https://www.cnblogs.com/xingbo/p/5498150.html
