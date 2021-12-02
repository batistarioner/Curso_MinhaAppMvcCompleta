using DevIO.Business.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevIO.App.Controllers
{
    public class BaseController : Controller
    {
        private readonly INotificador _notificador;

        public BaseController(INotificador notificador)
        {
            _notificador = notificador;
        }

        public bool OperacaoValida()
        {
            return !_notificador.TemNotificacao();
        }
    }
}
