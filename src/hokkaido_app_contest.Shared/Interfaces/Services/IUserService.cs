using hokkaido_app_contest.Shared.Models.Entities;
using MagicOnion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hokkaido_app_contest.Shared.Interfaces.Services {
    public interface IUserService : IService<IUserService> {
        /// <summary>
        /// 接続テスト
        /// </summary>
        UnaryResult<User[]> ConnectionTest();
    }
}
