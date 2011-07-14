using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PraLoup.Infrastructure.Logging
{
    public interface ILogger
    {
        /// <summary>
        /// Debugs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Debug(string message);

        /// <summary>
        /// Debugs the specified message.
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="objs">message parameters</param>
        void Debug(string message, params object[] objs);

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="e">The e.</param>
        void Error(string message, System.Exception e);

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Error(string message);

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="objs"></param>
        void Error(string message, params object[] objs);

        /// <summary>
        /// Infoes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Info(string message);

        /// <summary>
        /// Infoes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="objs"></param>
        void Info(string message, params object[] objs);

        /// <summary>
        /// Warns the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Warn(string message);

        /// <summary>
        /// Warns the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="objs"></param>
        void Warn(string message, params object[] objs);
    }
}
