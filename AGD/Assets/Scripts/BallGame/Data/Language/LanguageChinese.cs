using System.Collections.Generic;

public class LanguageChinese : Language
{

    public LanguageChinese() : base()
    {

    }

    /* Fills the dictionary of IDs and texts for this language */
    override public void FillDictionary()
    {
        _languageDictionary = new Dictionary<string, string>();

        _languageDictionary.Add(LanguageTextIDs.YES, "是的");
        _languageDictionary.Add(LanguageTextIDs.NO, "沒有");
        _languageDictionary.Add(LanguageTextIDs.OK, "行");
        _languageDictionary.Add(LanguageTextIDs.CANCEL, "取消");
        _languageDictionary.Add(LanguageTextIDs.CLOSE, "關閉");

        // General
        _languageDictionary.Add(LanguageTextIDs.START, "啟動");
        _languageDictionary.Add(LanguageTextIDs.RESTART, "重新開始");
        _languageDictionary.Add(LanguageTextIDs.CONTINUE, "連續");
        _languageDictionary.Add(LanguageTextIDs.BACK, "背部");
        _languageDictionary.Add(LanguageTextIDs.LEVEL, "水平");
        _languageDictionary.Add(LanguageTextIDs.CORRECT, "權");
        _languageDictionary.Add(LanguageTextIDs.INCORRECT, "不正確");
        _languageDictionary.Add(LanguageTextIDs.SCORE, "比分");
        _languageDictionary.Add(LanguageTextIDs.BONUS, "獎金");
        _languageDictionary.Add(LanguageTextIDs.TOTAL, "總");
        _languageDictionary.Add(LanguageTextIDs.HIGHSCORE, "排行榜");
        _languageDictionary.Add(LanguageTextIDs.QUIT, "相當");
        _languageDictionary.Add(LanguageTextIDs.MULTIPLIER, "乘數");


        // FeedBack Positive
        _languageDictionary.Add(LanguageTextIDs.FEEDPOS1, "大");
        _languageDictionary.Add(LanguageTextIDs.FEEDPOS2, "真棒");
        _languageDictionary.Add(LanguageTextIDs.FEEDPOS3, "WOW");
        _languageDictionary.Add(LanguageTextIDs.FEEDPOS4, "KAPOW");
        _languageDictionary.Add(LanguageTextIDs.FEEDPOS5, "極好");
        _languageDictionary.Add(LanguageTextIDs.FEEDPOS6, "天才");

        // FeedBack Positive
        _languageDictionary.Add(LanguageTextIDs.FEEDNEG1, "OOPS");
        _languageDictionary.Add(LanguageTextIDs.FEEDNEG2, "錯誤");
        _languageDictionary.Add(LanguageTextIDs.FEEDNEG3, "不正確");
        _languageDictionary.Add(LanguageTextIDs.FEEDNEG4, "OH NO");
        _languageDictionary.Add(LanguageTextIDs.FEEDNEG5, "白痴");

        //Phase
        _languageDictionary.Add(LanguageTextIDs.PHASESTART, "階段開始");
        _languageDictionary.Add(LanguageTextIDs.PHASEEND, "階段結束");

    }

}
