namespace AdventOfCode.DayFour
{
  [System.Serializable]
  public class PassportField
  {
    public string Name;
    public string FieldID;
    public bool Required;

    private string content;
    private bool hasContents;

    public PassportField()
    {

    }

    public PassportField(PassportField copy)
    {
      FieldID = copy.FieldID;
      Required = copy.Required;
    }

    public bool IsValid()
    {
      return !Required || hasContents;
    }

    public void SetContent(string newContent)
    {
      hasContents = !string.IsNullOrEmpty(newContent);
      content = newContent;
    }
  }
}