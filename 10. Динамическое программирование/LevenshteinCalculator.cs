using System;
using System.Configuration;
using System.Collections.Generic;

// Каждый документ — это список токенов. То есть List<string>.
// Вместо этого будем использовать псевдоним DocumentTokens.
// Это поможет избежать сложных конструкций:
// вместо List<List<string>> будет List<DocumentTokens>
using DocumentTokens = System.Collections.Generic.List<string>;

namespace Antiplagiarism
{
    public class LevenshteinCalculator
    {
        public List<ComparisonResult> CompareDocumentsPairwise(List<DocumentTokens> documents)
        {
            List<ComparisonResult> resList = new List<ComparisonResult>();

            for (int i = 0; i < documents.Count; i++)
            {
                for (int j = i + 1; j < documents.Count; j++)
                {
                    resList.Add(CompareDocuments(documents[i], documents[j]));
                }
            }

            return resList;
        }

        public ComparisonResult CompareDocuments(DocumentTokens firstDocument, DocumentTokens secondDocument)
        {
            var opt = new double[firstDocument.Count + 1, secondDocument.Count + 1];
            for (var i = 0; i <= firstDocument.Count; ++i)
                opt[i, 0] = i;
            for (var i = 0; i <= secondDocument.Count; ++i)
                opt[0, i] = i;
            for (var i = 1; i <= firstDocument.Count; ++i)
                for (var j = 1; j <= secondDocument.Count; ++j)
                {
                    if (firstDocument[i - 1] == secondDocument[j - 1])
                        opt[i, j] = opt[i - 1, j - 1];
                    else
                        opt[i, j] = opt[i - 1, j - 1] +
TokenDistanceCalculator.GetTokenDistance(firstDocument[i - 1], secondDocument[j - 1]);

                    opt[i, j] = Math.Min(opt[i, j], (1 + opt[i - 1, j]));
                    opt[i, j] = Math.Min(opt[i, j], (1 + opt[i, j - 1]));
                }
            return new ComparisonResult(firstDocument, secondDocument, opt[firstDocument.Count, secondDocument.Count]);
        }
    }
}

