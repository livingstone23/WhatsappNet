namespace WhatsapNet.Api.Util
{
    public class Util: IUtil
    {
        public object TextMessage(string message, string number)
        {
            return new
            {
                messaging_product = "whatsapp",
                to = number,
                type = "text",
                text = new
                {
                    body = message
                }
            };
        }


        public object ImageMessage(string url, string number)
        {
            return new
            {
                messaging_product = "whatsapp",
                to = number,
                type = "image",
                image = new
                {
                    link = url
                }
            };
        }


        public object AudioMessage(string url, string number)
        {
            return new
            {
                messaging_product = "whatsapp",
                to = number,
                type = "audio",
                audio = new
                {
                    link = url
                }
            };
        }


        public object VideoMessage(string url, string number)
        {
            return new
            {
                messaging_product = "whatsapp",
                to = number,
                type = "video",
                video = new
                {
                    link = url
                }
            };
        }


        public object DocumentMessage(string url, string number)
        {
            return new
            {
                messaging_product = "whatsapp",
                to = number,
                type = "document",
                document = new
                {
                    link = url
                }
            };
        }


        public object LocationMessage(string number)
        {
            return new
            {
                messaging_product = "whatsapp",
                to = number,
                type = "location",
                location = new
                {
                    latitude = "40.449727",
                    longitude = "-3.623424",
                    name = "Provimad",
                    address = "C. de Miguel Yuste, 18, Planta 4ª oficina 1, 28037 Madrid"
                }
            };
        }


        public object ButtonMessage(string number)
        {
            return new
            {
                messaging_product = "whatsapp",
                to = number,
                type = "interactive",
                interactive = new
                {
                    type ="button",
                    body = new
                    {
                        text = "Bienvenido a PROVIMAD gracias por solicitar nuestro servicio de WhatsApp ¿En que podemos ayudarte? para atender su solicitud es muy importante que mantenga una comunicación  al Seleccionar una opción"
                    },
                    action = new
                    {
                        buttons = new List<object>
                        {
                            new
                            {
                                type = "reply",
                                reply = new
                                {
                                    id = "01",
                                    title = "Localización"
                                }
                            },
                            new
                            {
                                type = "reply",
                                reply = new
                                {
                                    id = "02",
                                    title = "Productos"
                                }
                            },new
                            {
                                type = "reply",
                                reply = new
                                {
                                    id = "03",
                                    title = "Atención_de_agente"
                                }
                            }
                        }
                    }
                }
            };
        }


    }


}
