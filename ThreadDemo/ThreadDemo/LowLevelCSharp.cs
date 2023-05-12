//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ThreadDemo
//{
//  // Decompiled with JetBrains decompiler
//// Type: ThreadDemo.ValueTaskTest
//// Assembly: ThreadDemo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: C9F6D13F-5B71-43E5-97E0-E40671B7A3EF
//// Assembly location: D:\workspace\github\Bins\ThreadDemo\ThreadDemo\bin\Debug\net6.0\ThreadDemo.dll
//// Local variable names from d:\workspace\github\bins\threaddemo\threaddemo\bin\debug\net6.0\threaddemo.pdb
//// Compiler-generated code is shown

//using System;
//using System.Diagnostics;
//using System.Runtime.CompilerServices;
//using System.Threading.Tasks;

//namespace ThreadDemo
//{
//  internal class ValueTaskTest
//  {
//    [AsyncStateMachine(typeof (ValueTaskTest.<GetValue>d__0))]
//    [DebuggerStepThrough]
//    public ValueTask GetValue()
//    {
//      ValueTaskTest.<GetValue>d__0 stateMachine = new ValueTaskTest.<GetValue>d__0();
//      stateMachine.<>t__builder = AsyncValueTaskMethodBuilder.Create();
//      stateMachine.<>4__this = this;
//      stateMachine.<>1__state = -1;
//      stateMachine.<>t__builder.Start<ValueTaskTest.<GetValue>d__0>(ref stateMachine);
//      return stateMachine.<>t__builder.Task;
//    }

//    [AsyncStateMachine(typeof (ValueTaskTest.<GetValue2>d__1))]
//    [DebuggerStepThrough]
//    public Task GetValue2()
//    {
//      ValueTaskTest.<GetValue2>d__1 stateMachine = new ValueTaskTest.<GetValue2>d__1();
//      stateMachine.<>t__builder = AsyncTaskMethodBuilder.Create();
//      stateMachine.<>4__this = this;
//      stateMachine.<>1__state = -1;
//      stateMachine.<>t__builder.Start<ValueTaskTest.<GetValue2>d__1>(ref stateMachine);
//      return stateMachine.<>t__builder.Task;
//    }

//    public ValueTaskTest()
//    {
//      base..ctor();
//    }

//    [CompilerGenerated]
//    private sealed class <GetValue2>d__1 : IAsyncStateMachine
//    {
//      public int <>1__state;
//      public AsyncTaskMethodBuilder <>t__builder;
//      public ValueTaskTest <>4__this;
//      private TaskAwaiter <>u__1;

//      public <GetValue2>d__1()
//      {
//        base..ctor();
//      }

//      void IAsyncStateMachine.MoveNext()
//      {
//        int num1 = this.<>1__state;
//        try
//        {
//          TaskAwaiter awaiter;
//          int num2;
//          if (num1 != 0)
//          {
//            awaiter = Task.Delay(100).GetAwaiter();
//            if (!awaiter.IsCompleted)
//            {
//              this.<>1__state = num2 = 0;
//              this.<>u__1 = awaiter;
//              ValueTaskTest.<GetValue2>d__1 stateMachine = this;
//              this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, ValueTaskTest.<GetValue2>d__1>(ref awaiter, ref stateMachine);
//              return;
//            }
//          }
//          else
//          {
//            awaiter = this.<>u__1;
//            this.<>u__1 = new TaskAwaiter();
//            this.<>1__state = num2 = -1;
//          }
//          awaiter.GetResult();
//        }
//        catch (Exception ex)
//        {
//          this.<>1__state = -2;
//          this.<>t__builder.SetException(ex);
//          return;
//        }
//        this.<>1__state = -2;
//        this.<>t__builder.SetResult();
//      }

//      [DebuggerHidden]
//      void IAsyncStateMachine.SetStateMachine([Nullable(1)] IAsyncStateMachine stateMachine)
//      {
//      }
//    }

//    [CompilerGenerated]
//    private sealed class <GetValue>d__0 : IAsyncStateMachine
//    {
//      public int <>1__state;
//      public AsyncValueTaskMethodBuilder <>t__builder;
//      public ValueTaskTest <>4__this;
//      private TaskAwaiter <>u__1;

//      public <GetValue>d__0()
//      {
//        base..ctor();
//      }

//      void IAsyncStateMachine.MoveNext()
//      {
//        int num1 = this.<>1__state;
//        try
//        {
//          TaskAwaiter awaiter;
//          int num2;
//          if (num1 != 0)
//          {
//            awaiter = Task.Delay(100).GetAwaiter();
//            if (!awaiter.IsCompleted)
//            {
//              this.<>1__state = num2 = 0;
//              this.<>u__1 = awaiter;
//              ValueTaskTest.<GetValue>d__0 stateMachine = this;
//              this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, ValueTaskTest.<GetValue>d__0>(ref awaiter, ref stateMachine);
//              return;
//            }
//          }
//          else
//          {
//            awaiter = this.<>u__1;
//            this.<>u__1 = new TaskAwaiter();
//            this.<>1__state = num2 = -1;
//          }
//          awaiter.GetResult();
//        }
//        catch (Exception ex)
//        {
//          this.<>1__state = -2;
//          this.<>t__builder.SetException(ex);
//          return;
//        }
//        this.<>1__state = -2;
//        this.<>t__builder.SetResult();
//      }

//      [DebuggerHidden]
//      void IAsyncStateMachine.SetStateMachine([Nullable(1)] IAsyncStateMachine stateMachine)
//      {
//      }
//    }
//  }
//}

//}
