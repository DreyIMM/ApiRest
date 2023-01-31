using DevIO.Business.Intefaces;
using DevIO.Business.Notificacoes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Linq;

namespace DevIO.Api.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {

        private readonly INotificador _notificador;
        public readonly IUser AppUser;

        protected Guid UsuarioId { get; set; }

        protected bool UsuarioAutenticado { get; set; }

        public MainController(INotificador notificador, IUser appUser)
        {
            _notificador = notificador;
            AppUser = appUser;

            if (appUser.IsAuthenticated())
            {
                UsuarioId = appUser.GetUserId();
                UsuarioAutenticado = true;
            }

        }

        protected bool Operacaovalida()
        {
            return !_notificador.TemNotificacao();
        }

        protected ActionResult CustomResponse(object result = null)
        {
            if (Operacaovalida())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                erros = _notificador.ObterNotificacoes().Select(n => n.Mensagem)
            }) ;
        }


        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!ModelState.IsValid)  NotificarErroModelInvalida(modelState);
            return CustomResponse();
            
        }

        protected void NotificarErroModelInvalida(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);

            foreach(var erro in erros)
            {
                var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotificarError(erroMsg);
            }
        }

        protected void NotificarError(string mesnsagem)
        {
            _notificador.Handle(new Notificacao(mesnsagem));
        }




        //Validação de modelState

        //Validacao da operacao de negocios

    }
}
