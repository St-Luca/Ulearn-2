using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{
    public class StatisticsTask
    {
        public static double GetMedianTimePerSlide(List<VisitRecord> visits, SlideType slideType)
        {
            return visits
                .GroupBy(visit => visit.UserId)//Группируем по пользователям
                .Select(visitRecords => //Выбрать такие слайды, что:
                {
                    return visitRecords
                    .OrderBy(visitRecord => visitRecord.DateTime) //Сортируем по времени
                    .Bigrams() //Составляем кортежи методом
                    .Where(tuple => tuple.Item1.SlideType == slideType) //Берем нужный тип слайда
                    .Select(tuple => (tuple.Item2.DateTime - tuple.Item1.DateTime).TotalMinutes)
                    //время между посещением в минутах
                    .Where(d => d >= 1 && d <= 120); //Нужный период
                })
                .SelectMany(res => res) //Преобразуем в Enumerable для медианы
                .DefaultIfEmpty(0) //Если лист пуст
                .Median();
        }
    }
}