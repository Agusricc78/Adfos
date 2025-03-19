using System;
using System.Collections.Generic;
using Adfos.DataAccessLayer;
using Adfos.Entities;

namespace Adfos.BusinessLogicLayer
{
    public class AppUserBl
    {
        readonly DataAccess _objDa = new DataAccess();

        /// <summary>
        /// Autentifica el usuario en el sistema
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int Authenticate(string userName, string password)
        {
            var retValue = 0;
            var user = _objDa.Get("AppUserGet", new AppUser() {UserName = userName,Password = password});
            if (user != null && user.Id > 0)
            {
                retValue = user.UnsuscribeDate == null ? user.Id : -1;
            }
            return retValue;


        }

        /// <summary>
        /// Obtiene lista de usuarios
        /// </summary>
        /// <returns></returns>
        public List<AppUser> GetList()
        {
            return _objDa.GetList("AppUserGetList", new AppUser());
        }

        /// <summary>
        /// Obtiene un usuario por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AppUser GetbyId(int id)
        {
            return _objDa.GetById<AppUser>("AppUserGetById",id);
        }

        /// <summary>
        /// Guarda datos de ingreso al sistema 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        public bool SaveLoginData(int userId,  string ip)
        {
            return _objDa.Execute("AppLoginDataIns", userId, ip );
        }

        /// <summary>
        /// Alta de usuario
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool Insert(string userName, string password, string name, string email, Guid token)
        {
            return _objDa.Execute("AppUserIns", new AppUser() { UserName = userName, Password = password, Name = name, Email = email, Token = token });
        }

        /// <summary>
        /// Actualizacion de usuario
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool Update(int userId, string password, string name, string email, Guid token)
        {
            return _objDa.Execute("AppUserUpd", new AppUser() { Id = userId, Password = password, Name = name, Email = email, Token = token });
        }

        /// <summary>
        /// SelfUpdate
        /// </summary>
        /// <param name="password"></param>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool SelfUpdate(string password, string name, string email, Guid token)
        {
            return _objDa.Execute("AppUserSelfUpd", new AppUser() {  Password = password, Name = name, Email = email, Token = token });
        }

        /// <summary>
        /// Baja de usuario
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool Delete(int id, Guid token)
        {
            return _objDa.Execute("AppUserDel", new AppUser() { Id = id, Token = token });
        }



        
    }
}
