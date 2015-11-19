using System;
using Kassandra.Events;

namespace Kassandra
{
    public interface ITransaction : IDisposable
    {
        ITransaction AppendCommand(ICommand command);
        void Commit();
        void Rollback();

        #region Events

        ITransaction OnRollbacking(Action<RollbackEventArgs> action);
        ITransaction OnRollbacked(Action<RollbackEventArgs> action);
        ITransaction OnCommiting(Action<CommitEventArgs> action);
        ITransaction OnCommited(Action<CommitEventArgs> action);

        #endregion
    }
}