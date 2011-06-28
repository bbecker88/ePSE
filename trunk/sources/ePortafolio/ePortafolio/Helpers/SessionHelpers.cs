using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace ePortafolio.Helpers
{
    public enum GlobalKey
    {
        /// <summary>
        /// String: u511477
        /// </summary>
        UsuarioId,
        /// <summary>
        /// String: MARCO BRUGGMANN
        /// </summary>
        Nombre,
        /// <summary>
        /// String: 20081
        /// </summary>
        ActualPeriodoId,
        /// <summary>
        /// Roles: Estudiante
        /// </summary>
        Rol,
        /// <summary>
        /// List<PeriodosBE>
        /// </summary>
        PeriodosMatriculados,
        /// <summary>
        /// String: ianache<PeriodosBE>
        /// </summary>
        CoordinadorId,
    }

    public enum Roles
    {
        Estudiante,
        Profesor,
        Coordinador
    }

    public static class SessionHelpers
    {
        #region Private

        private static object Get(HttpSessionState Session, String Key)
        {
            return Session[Key];
        }

        private static void Set(HttpSessionState Session, String Key, object Value)
        {
            Session[Key] = Value;
        }

        private static bool Exists(HttpSessionState Session, String Key)
        {
            return Session[Key] != null;
        }

        private static object Get(HttpSessionStateBase Session, String Key)
        {
            return Session[Key];
        }

        private static void Set(HttpSessionStateBase Session, String Key, object Value)
        {
            Session[Key] = Value;
        }

        private static bool Exists(HttpSessionStateBase Session, String Key)
        {
            return Session[Key] != null;
        }

        #endregion

        #region Getters setters GlobalKey
        //HttpSessionState
        public static object Get(this HttpSessionState Session, GlobalKey Key)
        {
            return Get(Session, Key.ToString());
        }

        public static void Set(this HttpSessionState Session, GlobalKey Key, object Value)
        {
            Set(Session, Key.ToString(), Value);
        }

        public static bool Exists(this HttpSessionState Session, GlobalKey Key)
        {
            return Exists(Session, Key.ToString());
        }

        //HttpSessionStateBase
        public static object Get(this HttpSessionStateBase Session, GlobalKey Key)
        {
            return Get(Session, Key.ToString());
        }

        public static void Set(this HttpSessionStateBase Session, GlobalKey Key, object Value)
        {
            Set(Session, Key.ToString(), Value);
        }

        public static bool Exists(this HttpSessionStateBase Session, GlobalKey Key)
        {
            return Exists(Session, Key.ToString());
        }
        #endregion
    }
}
