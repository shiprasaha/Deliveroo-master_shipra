using System;
using System.Data;
using Deliveroo.Cron.Exceptions;
using Deliveroo.Cron.Service;

namespace Deliveroo.Cron.Controllers
{
    public class CronApplicationController
    {
        private CronExpressionParserService cronExpressionParserService;
        public CronApplicationController()
        {
            cronExpressionParserService = new CronExpressionParserService();
        }
        public string ParseExpression(String notation)
        {
            if (string.IsNullOrEmpty(notation))
            {
                throw new InvalidExpressionException("Invalid arguments provided");
            }

            try
            {
                CronExpressionResult cronExpressionResult = cronExpressionParserService.Parse(notation);
                var cronExpressionResultFormatter = new CronExpressionResultFormatter();
                var formattedCronExpression = cronExpressionResultFormatter.Format(cronExpressionResult);

                return formattedCronExpression;
            }
            catch (Exception ex)
            {
                throw new ExpressionParsingException("Parsing Exception", ex);
            }
        }
    }
}
