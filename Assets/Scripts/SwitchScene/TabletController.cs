using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using System.IO;
using System.Text;
using iTextSharp.awt.geom;
using iTextSharp.text;
using iTextSharp.text.pdf;
using UnityEngine.UI;
using Font = iTextSharp.text.Font;
using UnityEngine.SceneManagement;
using Button = UnityEngine.UI.Button;
using Debug = UnityEngine.Debug;
using Image = iTextSharp.text.Image;

public class TabletController : MonoBehaviour
{
	public bool isLocked;
    [SerializeField] private FirstPersonControllerTim  maincamera;
    public GameObject layout; 
    public GameObject layoutCalculator;
    public GameObject tablet;
    public GameObject table_of_button; 
    public GameObject tabtablettask, tabtablettheory, tabtablettable;
    bool CalculatorEnable=false;
    public GameObject crosshair;
    public GameObject using_pause;
    public bool in_tablet = false;

    public Button btn_task;
    public Button btn_theor;
    public Button btn_table;
    void SetCursorLock(bool isLocked)
    {
        this.isLocked = isLocked;
        Screen.lockCursor = isLocked;
        Cursor.visible = !isLocked;
    }
    void settab(bool tabtask, bool tabtheory, bool tabtable)
    {
	    btn_task.interactable = !tabtask;
	    btn_theor.interactable = !tabtheory;
	    btn_table.interactable = !tabtable;
        tabtablettask.gameObject.SetActive(tabtask);
        tabtablettheory.gameObject.SetActive(tabtheory);
        tabtablettable.gameObject.SetActive(tabtable);
    }

    void Start()
    {	//lock fps
	    //Application.targetFrameRate = 144;
	    //lock fps
        Renderer rend = gameObject.GetComponent<Renderer>();
        //rend.enabled = false;
        SetCursorLock(true);
        layoutCalculator.gameObject.SetActive(false);
        layout.gameObject.SetActive(false);
        settab(true, false, false);
    }
    public void Click1()
    {
        settab(false, true, false);
    }
    public void Click2()
    {
        settab(true, false, false);
    }
    public void Click3()
    {
        settab(false, false, true);
    }
    public void Click4()
    {
	    //Debug.Log("btn2");

        
        if (!CalculatorEnable){
            tablet.transform.Translate(-270, 0, 0);
        }
        else
        {
            tablet.transform.Translate(270, 0, 0);
        }
        CalculatorEnable = !CalculatorEnable;
        layoutCalculator.gameObject.SetActive(CalculatorEnable);
    }
    public static Transform SearchChild(Transform tr, string name)
    {
	    //Для всех детей
	    //Debug.Log(tr.name);
	    for(int i = 0; i < tr.childCount; i++)
	    {
		    //Берем очередного ребенка
		    Transform tt = tr.GetChild(i);
		    //Если имя совпало - сразу возвращаемся
		    if (tt.name == name) return tt;
		    //Если у него нет детей - к следующему ребенку
		    if (tt.childCount == 0) continue;
		    //Дети у ребенка есть - пытаемся найти среди них
		    Transform ti = SearchChild(tt, name);
		    //Нашли среди детей очередного ребенка! - возвращаем находку
		    if (ti != null) return ti;
	    }
	    //Всех детей и их потомков просмотрели, но ничего не нашли.
	    return null;
    }
    public void PDF_click()
    {
	    
        string path = Application.persistentDataPath + "/LR1.pdf";
        Document doc = new Document();
        try {
            PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));
        } catch (System.Exception e) {
            Debug.LogError(e.ToString());
        }
        BaseFont baseFont = BaseFont.CreateFont(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "TIMES.TTF"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
		doc.Open(); 
	    //Debug.Log(SearchChild(table_of_button.transform,"2.2").GetComponent<InputField>().text);
	    Paragraph fio = new Paragraph(SearchChild(table_of_button.transform,"fio").GetComponent<InputField>().text,new Font(baseFont, 16, Font.BOLD, BaseColor.BLACK));
		fio.Alignment = Element.ALIGN_RIGHT;
		Paragraph date = new Paragraph(System.DateTime.Now.ToString("hh:mm:ss dd/MM/yyyy"),new Font(baseFont, 16, Font.BOLD, BaseColor.BLACK));
		date.Alignment = Element.ALIGN_RIGHT;
		date.SpacingAfter = 12f;
		doc.Add(fio);
		doc.Add(date);
		var title = new Paragraph ("Лабораторная работа № 1\nЗАКОНЫ ДИНАМИКИ ПОСТУПАТЕЛЬНОГО ДВИЖЕНИЯ", new Font(baseFont, 18, Font.BOLD, BaseColor.BLACK));
		title.Alignment = Element.ALIGN_CENTER;
		doc.Add(title);
		Paragraph purpose = new Paragraph("\nЦель работы: определить силу упругости подвеса и среднюю силу удара.",
			new Font(baseFont, 14, Font.NORMAL, BaseColor.BLACK));
		purpose.IndentationLeft = 40f;
		doc.Add(purpose);
		Paragraph devices = new Paragraph("Приборы и принадлежности: лабораторная установка для исследования",
			new Font(baseFont, 14, Font.NORMAL, BaseColor.BLACK));
		devices.IndentationLeft = 40f;
		doc.Add(devices);
		doc.Add(new Paragraph("соударений, вольтметр, устройство для измерения времени соударения, штан-генциркуль.",
			new Font(baseFont, 14, Font.NORMAL, BaseColor.BLACK)));
		Paragraph p = new Paragraph(" \n Выполнение работы ",new Font(baseFont, 16, Font.BOLD, BaseColor.BLACK));
		p.Alignment = Element.ALIGN_CENTER;
		doc.Add(p);
		Paragraph p1 = new Paragraph(" Результаты измерений ",new Font(baseFont, 14, Font.NORMAL, BaseColor.BLACK));
		p1.Alignment = Element.ALIGN_CENTER;
		p1.SpacingAfter = 12f;
		doc.Add(p1);
		int size_col = 9;
		int size_row = 5;
		string[] sign = new[] {"№","d","Δd","a₂","Δa₂","φ","Δφ","φ₀","Δφ₀"};
		string[] l_col = new[] {"1","2","3","Сред. знач."};
		
		PdfPTable table = new PdfPTable(size_col);
		for (int i = 0; i < size_col; i++)
		{
			PdfPCell cell = new PdfPCell();
			cell = new PdfPCell(new Phrase(new Phrase(sign[i], new Font(baseFont, 14, Font.NORMAL, BaseColor.BLACK))));
			cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
			cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
			table.AddCell(cell);
		}
		for (int i  = 0; i  < size_row-1; i ++)
		{
			for (int j = -1; j < size_col-1; j++)
			{
				PdfPCell cell = new PdfPCell();
				if (j == -1)
				{
					cell = new PdfPCell(new Phrase(l_col[i], new Font(baseFont, 14, Font.NORMAL, BaseColor.BLACK)));
				}
				else
				{
					cell = new PdfPCell(new Phrase(new Phrase(SearchChild(table_of_button.transform,i.ToString()+"_"+j.ToString()).GetComponent<InputField>().text, new Font(baseFont, 14, Font.NORMAL, BaseColor.BLACK))));

				}
				cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
				cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
				table.AddCell(cell);
			}
			
		}
		table.HorizontalAlignment = Element.ALIGN_CENTER;
		table.SpacingAfter = 12f;
		doc.Add(table);
		string[] sign_tb2 = new[] {"R±ΔR","d±Δd","a₁±Δa₁","a₂±Δa₂","φ±Δφ","φ₀±Δφ₀"};
		PdfPTable table4 = new PdfPTable(6);
		for (int i = 0; i < 6; i++)
		{  
			PdfPCell cell = new PdfPCell();

			cell = new PdfPCell(new Phrase(sign_tb2[i], new Font(baseFont, 14, Font.NORMAL, BaseColor.BLACK)));
			cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
			cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
			table4.AddCell(cell);
		}
		for (int i  = 0; i  < 6; i ++)
		{
			PdfPCell cell = new PdfPCell();

			
			cell = new PdfPCell(new Phrase(SearchChild(table_of_button.transform,"2mod ("+i.ToString()+")").GetComponent<InputField>().text, new Font(baseFont, 14, Font.NORMAL, BaseColor.BLACK)));
			cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
			cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
			table4.AddCell(cell);
		}
		table4.HorizontalAlignment = Element.ALIGN_CENTER;
		doc.Add(table4);
		Paragraph p2 = new Paragraph(" Результаты расчетов ",new Font(baseFont, 14, Font.NORMAL, BaseColor.BLACK));
		p2.Alignment = Element.ALIGN_CENTER;
		p2.SpacingAfter = 12f;
		doc.Add(p2);
		string[] sign_tb1 = new[] {"m","M","V₁","V₂","t₀","Δt","Fᵧ₁","Fᵧ₂","Fср"};
		PdfPTable table1 = new PdfPTable(9);
		for (int i = 0; i < 9; i++)
		{
			PdfPCell cell = new PdfPCell();

			cell = new PdfPCell(new Phrase(sign_tb1[i], new Font(baseFont, 14, Font.NORMAL, BaseColor.BLACK)));
			cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
			cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
			table1.AddCell(cell);
		}
		for (int i  = 0; i  < 9; i ++)
		{
			PdfPCell cell = new PdfPCell();
			cell = new PdfPCell(new Phrase(SearchChild(table_of_button.transform,"3mod_8 ("+i.ToString()+")").GetComponent<InputField>().text, new Font(baseFont, 14, Font.NORMAL, BaseColor.BLACK)));
			cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
			cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
			table1.AddCell(cell);
		}
		table1.HorizontalAlignment = Element.ALIGN_CENTER;
		doc.Add(table1);
		Paragraph p3 = new Paragraph(" \n Вывод ",new Font(baseFont, 16, Font.BOLD, BaseColor.BLACK));
		p3.Alignment = Element.ALIGN_CENTER;
		p3.SpacingAfter = 12f;
		doc.Add(p3);
		doc.Add(new Paragraph(SearchChild(table_of_button.transform,"5mod").GetComponent<InputField>().text,
			new Font(baseFont, 14, Font.NORMAL, BaseColor.BLACK)));
		doc.Close();
		Application.OpenURL("file:///"+path);
    }

    public void PDF_click_lr7()
    {
	    string path = Application.persistentDataPath + "/LR7.pdf";
        Document doc = new Document();
        try {
            PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));
        } catch (System.Exception e) {
            Debug.LogError(e.ToString());
        }
        BaseFont baseFont = BaseFont.CreateFont(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "TIMES.TTF"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
		doc.Open(); 
	    //Debug.Log(SearchChild(table_of_button.transform,"2.2").GetComponent<InputField>().text);
	    Paragraph fio = new Paragraph(SearchChild(table_of_button.transform,"fio").GetComponent<InputField>().text,new Font(baseFont, 16, Font.BOLD, BaseColor.BLACK));
		fio.Alignment = Element.ALIGN_RIGHT;
		Paragraph date = new Paragraph(System.DateTime.Now.ToString("hh:mm:ss dd/MM/yyyy"),new Font(baseFont, 16, Font.BOLD, BaseColor.BLACK));
		date.Alignment = Element.ALIGN_RIGHT;
		date.SpacingAfter = 12f;
		doc.Add(fio);
		doc.Add(date);
		var title = new Paragraph ("Лабораторная работа № 7\nПРОВОДНИКИ В ЭЛЕКТРИЧЕСКОМ ПОЛЕ", new Font(baseFont, 18, Font.BOLD, BaseColor.BLACK));
		title.Alignment = Element.ALIGN_CENTER;
		doc.Add(title);
		Paragraph purpose = new Paragraph("\nЦель работы: определить электроемкость конденсаторов; экспериментально ",
			new Font(baseFont, 14, Font.NORMAL, BaseColor.BLACK));
		purpose.IndentationLeft = 40f;
		doc.Add(purpose);
		doc.Add(new Paragraph("проверить формулы для параллельного и последовательного соединения конденсаторов; определить энергию заряженных конденсаторов.",
			new Font(baseFont, 14, Font.NORMAL, BaseColor.BLACK)));
		Paragraph devices = new Paragraph("Приборы и принадлежности: гальванометр; источник тока; панель с ",
			new Font(baseFont, 14, Font.NORMAL, BaseColor.BLACK));
		devices.IndentationLeft = 40f;
		doc.Add(devices);
		doc.Add(new Paragraph("вольтметром и переключателями; конденсаторы.",
			new Font(baseFont, 14, Font.NORMAL, BaseColor.BLACK)));
		Paragraph p = new Paragraph(" \n Выполнение работы ",new Font(baseFont, 16, Font.BOLD, BaseColor.BLACK));
		p.Alignment = Element.ALIGN_CENTER;
		doc.Add(p);
		Paragraph p1 = new Paragraph(" Результаты измерений ",new Font(baseFont, 14, Font.NORMAL, BaseColor.BLACK));
		p1.Alignment = Element.ALIGN_CENTER;
		p1.SpacingAfter = 12f;
		doc.Add(p1);
		int size_col = 11;
		int size_row = 9;
		string[] sign = new[] {"С","№","U, В","n₀, мм","n, мм","n-n₀, мм","q=B(n-n₀), Кл","С=q/U","Cср","ΔСᵢ = Сᵢ-Сср","Cоткл"};

		PdfPTable table = new PdfPTable(size_col);
		PdfPCell cell;
		for (int i = 0; i < size_col; i++)
		{
			cell = new PdfPCell(new Phrase(new Phrase(sign[i], new Font(baseFont, 14, Font.NORMAL, BaseColor.BLACK))));
			cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
			cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
			table.AddCell(cell);
		}
		cell = new PdfPCell(new Phrase("C1", new Font(baseFont, 14, Font.NORMAL, BaseColor.BLACK)));
		cell.Rowspan = 3;
		cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
		cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
		table.AddCell(cell);
		for (int g = 0; g < 3; g++)
		{
			for (int i = 1; i < size_col; i++)
			{
				if (i == 1)
				{
					cell = new PdfPCell(new Phrase((g+1).ToString(), new Font(baseFont, 14, Font.NORMAL, BaseColor.BLACK)));
					cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
					cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
					table.AddCell(cell);
				}
				else if (i == 8 || i==10 )
				{
					if (g == 0)
					{
						cell = new PdfPCell(new Phrase(SearchChild(table_of_button.transform,i.ToString()+"_"+g.ToString()).GetComponent<InputField>().text, new Font(baseFont, 14, Font.NORMAL, BaseColor.BLACK)));
						cell.Rowspan = 3;
						cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
						cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
						table.AddCell(cell);
					}
					
				}
				else
				{
					cell = new PdfPCell(new Phrase(SearchChild(table_of_button.transform,i.ToString()+"_"+g.ToString()).GetComponent<InputField>().text, new Font(baseFont, 14, Font.NORMAL, BaseColor.BLACK)));
					cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
					cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
					table.AddCell(cell);
				}

			}

		}
		cell = new PdfPCell(new Phrase("C2", new Font(baseFont, 14, Font.NORMAL, BaseColor.BLACK)));
		cell.Rowspan = 3;
		cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
		cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
		table.AddCell(cell);
		for (int g = 3; g < 6; g++)
		{
			for (int i = 1; i < size_col; i++)
			{
				if (i == 1)
				{
					cell = new PdfPCell(new Phrase((g-2).ToString(), new Font(baseFont, 14, Font.NORMAL, BaseColor.BLACK)));
					cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
					cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
					table.AddCell(cell);
				}
				else if (i == 8 || i==10 )
				{
					if (g ==3)
					{
						cell = new PdfPCell(new Phrase(SearchChild(table_of_button.transform,i.ToString()+"_"+g.ToString()).GetComponent<InputField>().text, new Font(baseFont, 14, Font.NORMAL, BaseColor.BLACK)));
						cell.Rowspan = 3;
						cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
						cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
						table.AddCell(cell);
					}
					
				}
				else
				{
					cell = new PdfPCell(new Phrase(SearchChild(table_of_button.transform,i.ToString()+"_"+g.ToString()).GetComponent<InputField>().text, new Font(baseFont, 14, Font.NORMAL, BaseColor.BLACK)));
					cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
					cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
					table.AddCell(cell);
				}
			}
		}
		cell = new PdfPCell(new Phrase("Cпар", new Font(baseFont, 14, Font.NORMAL, BaseColor.BLACK)));
		cell.Colspan = 2;
		cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
		cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
		table.AddCell(cell);
		for (int i = 2; i < size_col; i++)
		{
			cell = new PdfPCell(new Phrase(SearchChild(table_of_button.transform,i.ToString()+"_"+7.ToString()).GetComponent<InputField>().text, new Font(baseFont, 14, Font.NORMAL, BaseColor.BLACK)));
			cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
			cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
			table.AddCell(cell);
		}
		cell = new PdfPCell(new Phrase("Cпосл", new Font(baseFont, 14, Font.NORMAL, BaseColor.BLACK)));
		cell.Colspan = 2;
		cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
		cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
		table.AddCell(cell);
		for (int i = 2; i < size_col; i++)
		{
			cell = new PdfPCell(new Phrase(SearchChild(table_of_button.transform,i.ToString()+"_"+8.ToString()).GetComponent<InputField>().text, new Font(baseFont, 14, Font.NORMAL, BaseColor.BLACK)));
			cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
			cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
			table.AddCell(cell);
		}
		table.HorizontalAlignment = Element.ALIGN_CENTER;
		doc.Add(table);
		Paragraph p3 = new Paragraph(" \n Вывод ",new Font(baseFont, 16, Font.BOLD, BaseColor.BLACK));
		p3.Alignment = Element.ALIGN_CENTER;
		p3.SpacingAfter = 12f;
		doc.Add(p3);
		doc.Add(new Paragraph(SearchChild(table_of_button.transform,"5mod").GetComponent<InputField>().text,
			new Font(baseFont, 14, Font.NORMAL, BaseColor.BLACK)));
		doc.Close();
		Application.OpenURL("file:///"+path);
    }
	public void PDF_click_lr8()
    {
	    
        string path = Application.persistentDataPath + "/LR8.pdf";
        Document doc = new Document();
        PdfWriter writer=PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));
        BaseFont baseFont = BaseFont.CreateFont(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "TIMES.TTF"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
		doc.Open(); 
	    //Debug.Log(SearchChild(table_of_button.transform,"2.2").GetComponent<InputField>().text);
	    Paragraph fio = new Paragraph(SearchChild(table_of_button.transform,"fio").GetComponent<InputField>().text,new Font(baseFont, 16, Font.BOLD, BaseColor.BLACK));
	    fio.Alignment = Element.ALIGN_RIGHT;
	    Paragraph date = new Paragraph(System.DateTime.Now.ToString("hh:mm:ss dd/MM/yyyy"),new Font(baseFont, 16, Font.BOLD, BaseColor.BLACK));
		date.Alignment = Element.ALIGN_RIGHT;
		date.SpacingAfter = 12f;
		doc.Add(fio);
		doc.Add(date);
		var title = new Paragraph ("Лабораторная работа № 8\nОПРЕДЕЛЕНИЕ ХАРАКТЕРИСТИК ИСТОЧНИКА ПОСТОЯННОГО ТОКА", new Font(baseFont, 18, Font.BOLD, BaseColor.BLACK));
		title.Alignment = Element.ALIGN_CENTER;
		doc.Add(title);
		Paragraph purpose = new Paragraph("\nЦель работы: определить электродвижущую силу и ток короткого ",
			new Font(baseFont, 14, Font.NORMAL, BaseColor.BLACK));
		purpose.IndentationLeft = 40f;
		doc.Add(purpose);
		doc.Add(new Paragraph("замыкания источника постоянного тока; исследовать зависимость полезной мощности источника от величины внешнего сопротивления.",
			new Font(baseFont, 14, Font.NORMAL, BaseColor.BLACK)));
		Paragraph devices = new Paragraph("Приборы и принадлежности: источник тока, вольтметр, амперметр,",
			new Font(baseFont, 14, Font.NORMAL, BaseColor.BLACK));
		devices.IndentationLeft = 40f;
		doc.Add(devices);
		doc.Add(new Paragraph("реостат, соединительные провода.",
			new Font(baseFont, 14, Font.NORMAL, BaseColor.BLACK)));
		Paragraph p = new Paragraph(" \n Выполнение работы ",new Font(baseFont, 16, Font.BOLD, BaseColor.BLACK));
		p.Alignment = Element.ALIGN_CENTER;
		doc.Add(p);
		Paragraph p1 = new Paragraph(" Результаты измерений ",new Font(baseFont, 14, Font.NORMAL, BaseColor.BLACK));
		p1.Alignment = Element.ALIGN_CENTER;
		p1.SpacingAfter = 6f;
		doc.Add(p1);
		int size_col = 13;
		int size_row = 4;
		PdfPTable table = new PdfPTable(size_col);
		string[] l_col = new[] {"U, B","I, A","Pᵣ, Вт","R, Ом"};
		string[,] coord_UI= new string[size_col-1,2];
		for (int i  = 0; i  < size_row; i ++)
		{
			for (int j = -1; j < size_col-1; j++)
			{
				PdfPCell cell = new PdfPCell();
				if (j == -1)
				{
					cell = new PdfPCell(new Phrase(l_col[i], new Font(baseFont, 14, Font.NORMAL, BaseColor.BLACK)));
				}
				else
				{
					cell = new PdfPCell(new Phrase(new Phrase(SearchChild(table_of_button.transform,i.ToString()+"_"+j.ToString()).GetComponent<InputField>().text, new Font(baseFont, 14, Font.NORMAL, BaseColor.BLACK))));
					if (i == 0)
					{
						coord_UI[j,1] = SearchChild(table_of_button.transform,i.ToString()+"_"+j.ToString()).GetComponent<InputField>().text;
					}
					if (i == 1)
					{
						coord_UI[j,0] = SearchChild(table_of_button.transform,i.ToString()+"_"+j.ToString()).GetComponent<InputField>().text;
					}
				}
				cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
				cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
				table.AddCell(cell);
			}
			
		}
		table.HorizontalAlignment = Element.ALIGN_CENTER;
		table.SpacingAfter = 12f;
		doc.Add(table);
		Paragraph p3 = new Paragraph(" \n Вывод ",new Font(baseFont, 16, Font.BOLD, BaseColor.BLACK));
		p3.Alignment = Element.ALIGN_CENTER;
		p3.SpacingAfter = 12f;
		doc.Add(p3);
		doc.Add(new Paragraph(SearchChild(table_of_button.transform,"5mod").GetComponent<InputField>().text,
			new Font(baseFont, 14, Font.NORMAL, BaseColor.BLACK)));
		doc.NewPage();
		doc.Add(new Paragraph("Построенный график", new Font(baseFont, 16, Font.BOLD, BaseColor.BLACK)));
		string figure_str = "";
		for (int i = 0; i < size_col - 1; i++)
		{
			figure_str += coord_UI[i, 0] + " " + coord_UI[i, 1] ;
			if (i != size_col - 2)
				figure_str += "\n";
		}
		try
		{
			if ( File.Exists(Application.dataPath+"/input_date_lr8.txt")) {
				File.Delete(Application.dataPath+"/input_date_lr8.txt");
			}
			if ( File.Exists(Application.dataPath+"/saved_figure_lr8.png")) {
				File.Delete(Application.dataPath+"/saved_figure_lr8.png");
			}
			using (FileStream fs = File.Create(Application.dataPath+"/input_date_lr8.txt"))
			{
				byte[] info = new UTF8Encoding(true).GetBytes(figure_str);
				fs.Write(info, 0, info.Length);
			}
			Process figure_create = new Process();
			figure_create.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			figure_create.StartInfo.FileName = Application.dataPath+"/figure_creator.exe";
			figure_create.StartInfo.Arguments = Application.dataPath;
			figure_create.Start();
			while (!File.Exists(Application.dataPath+"/saved_figure_lr8.png"))
			{
				System.Threading.Thread.Sleep(100);
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.ToString());
		}
		Image img = Image.GetInstance(Application.dataPath+"/saved_figure_lr8.png");
 	
		float percentage = 1;
		// Это оригинальная ширина и высота картинки  
		float resizedWidht = img.Width;
		float resizedHeight = img.Height;
 	
		// В настоящее время оцениваем, больше ли ширина изображения, чем ширина страницы минус поле, если оно есть, то уменьшаем, если оно еще больше, продолжаем уменьшать,  
		// Этот процент усадки будет становиться все меньше и меньше  
		while (resizedWidht > (doc.PageSize.Width - doc.LeftMargin - doc.RightMargin) * 0.8)
		{
			percentage = percentage * 0.9f;
			resizedHeight = img.Height * percentage;
			resizedWidht = img.Width * percentage;
		}
 	
		while (resizedHeight > (doc.PageSize.Height - doc.TopMargin - doc.BottomMargin) * 0.8)
		{
			percentage = percentage * 0.9f;
			resizedHeight = img.Height * percentage;
			resizedWidht = img.Width * percentage;
		}
 	
		// Здесь мы используем рассчитанный процент, чтобы уменьшить изображение  
		img.ScalePercent(percentage * 100);
		// Позиционирование изображения, общая ширина страницы составляет 283, а высота равна 416. Если вы установите здесь 0,0, это будет левый нижний угол страницы, так что центральная точка изображения будет совпадать с центральным хранилищем страницы.  
		img.SetAbsolutePosition(doc.PageSize.Width / 2 - resizedWidht / 2, doc.PageSize.Height / 2 - resizedHeight / 2);
		writer.DirectContent.AddImage(img);
		doc.Close();
		Application.OpenURL("file:///"+path);
    }
    public void PDF_click_lr9()
    {
	    
        string path = Application.persistentDataPath + "/LR9.pdf";
        Document doc = new Document();
        try {
            PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));
        } catch (System.Exception e) {
            Debug.LogError(e.ToString());
        }
        BaseFont baseFont = BaseFont.CreateFont(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "TIMES.TTF"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
		doc.Open(); 
	    //Debug.Log(SearchChild(table_of_button.transform,"2.2").GetComponent<InputField>().text);
	    Paragraph fio = new Paragraph(SearchChild(table_of_button.transform,"fio").GetComponent<InputField>().text,new Font(baseFont, 16, Font.BOLD, BaseColor.BLACK));
		fio.Alignment = Element.ALIGN_RIGHT;
		Paragraph date = new Paragraph(System.DateTime.Now.ToString("hh:mm:ss dd/MM/yyyy"),new Font(baseFont, 16, Font.BOLD, BaseColor.BLACK));
		date.Alignment = Element.ALIGN_RIGHT;
		date.SpacingAfter = 12f;
		doc.Add(fio);
		doc.Add(date);
		var title = new Paragraph ("Лабораторная работа № 9\nИССЛЕДОВАНИЕ ЭЛЕКТРИЧЕСКИХ ПОЛЕЙ В ЭЛЕКТРОННО-ЛУЧЕВОЙ ТРУБКЕ", new Font(baseFont, 18, Font.BOLD, BaseColor.BLACK));
		title.Alignment = Element.ALIGN_CENTER;
		doc.Add(title);
		Paragraph purpose = new Paragraph("\nЦель работы: изучить движение зарядов в электрических полях;",
			new Font(baseFont, 14, Font.NORMAL, BaseColor.BLACK));
		purpose.IndentationLeft = 40f;
		doc.Add(purpose);
		doc.Add(new Paragraph("ознакомиться с принципом действия и работой осциллографа; измерить амплитуды и периоды исследуемых сигналов.",
			new Font(baseFont, 14, Font.NORMAL, BaseColor.BLACK)));
		Paragraph devices = new Paragraph("Приборы и принадлежности: электронно-лучевой осциллограф С1-83; ",
			new Font(baseFont, 14, Font.NORMAL, BaseColor.BLACK));
		devices.IndentationLeft = 40f;
		doc.Add(devices);
		doc.Add(new Paragraph("генератор звуковой частоты; соединительные провода.",
			new Font(baseFont, 14, Font.NORMAL, BaseColor.BLACK)));
		Paragraph p = new Paragraph(" \n Выполнение работы ",new Font(baseFont, 16, Font.BOLD, BaseColor.BLACK));
		p.Alignment = Element.ALIGN_CENTER;
		doc.Add(p);
		doc.Add(new Paragraph(SearchChild(table_of_button.transform,"5mod").GetComponent<InputField>().text,
			new Font(baseFont, 14, Font.NORMAL, BaseColor.BLACK)));
		doc.Close();
		Application.OpenURL("file:///"+path);
    }

    public void PDF_click_lr10()
    {
	     string path = Application.persistentDataPath + "/LR10.pdf";
        Document doc = new Document();
        PdfWriter writer=PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));
        try {
            PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));
        } catch (System.Exception e) {
            Debug.LogError(e.ToString());
        }
        BaseFont baseFont = BaseFont.CreateFont(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "TIMES.TTF"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
		doc.Open(); 
	    //Debug.Log(SearchChild(table_of_button.transform,"2.2").GetComponent<InputField>().text);
	    Paragraph fio = new Paragraph(SearchChild(table_of_button.transform,"fio").GetComponent<InputField>().text,new Font(baseFont, 16, Font.BOLD, BaseColor.BLACK));
	    fio.Alignment = Element.ALIGN_RIGHT;
	    Paragraph date = new Paragraph(System.DateTime.Now.ToString("hh:mm:ss dd/MM/yyyy"),new Font(baseFont, 16, Font.BOLD, BaseColor.BLACK));
		date.Alignment = Element.ALIGN_RIGHT;
		date.SpacingAfter = 12f;
		doc.Add(fio);
		doc.Add(date);
		var title = new Paragraph ("Лабораторная работа № 10\nИЗУЧЕНИЕ СВОЙСТВ ПОЛЯРНЫХ ДИЭЛЕКТРИКОВ. СЕГНЕТОЭЛЕКТРИКИ", new Font(baseFont, 18, Font.BOLD, BaseColor.BLACK));
		title.Alignment = Element.ALIGN_CENTER;
		doc.Add(title);
		Paragraph purpose = new Paragraph("\nЦель работы: изучить свойства полярных диэлектриков; построить петлю ",
			new Font(baseFont, 14, Font.NORMAL, BaseColor.BLACK));
		purpose.IndentationLeft = 40f;
		doc.Add(purpose);
		doc.Add(new Paragraph("диэлектрического гистерезиса.",
			new Font(baseFont, 14, Font.NORMAL, BaseColor.BLACK)));
		Paragraph devices = new Paragraph("Приборы и принадлежности: установка для исследования ",
			new Font(baseFont, 14, Font.NORMAL, BaseColor.BLACK));
		devices.IndentationLeft = 40f;
		doc.Add(devices);
		doc.Add(new Paragraph("сегнетоэлектриков; осциллограф.",
			new Font(baseFont, 14, Font.NORMAL, BaseColor.BLACK)));
		Paragraph p = new Paragraph(" \n Выполнение работы ",new Font(baseFont, 16, Font.BOLD, BaseColor.BLACK));
		p.Alignment = Element.ALIGN_CENTER;
		doc.Add(p);
		Paragraph p1 = new Paragraph(" Результаты измерений ",new Font(baseFont, 14, Font.NORMAL, BaseColor.BLACK));
		p1.Alignment = Element.ALIGN_CENTER;
		p1.SpacingAfter = 6f;
		doc.Add(p1);
		int size_col = 12;
		int size_row = 6;
		PdfPTable table = new PdfPTable(size_col);
		string[] l_col = new[] {"U, B","X, мм","Y, мм","E, В/м","P, Кл/м²","ε"};
		for (int i  = 0; i  < size_row; i ++)
		{
			for (int j = -1; j < size_col-1; j++)
			{
				PdfPCell cell = new PdfPCell();
				if (j == -1)
				{
					cell = new PdfPCell(new Phrase(l_col[i], new Font(baseFont, 14, Font.NORMAL, BaseColor.BLACK)));
				}
				else
				{
					cell = new PdfPCell(new Phrase(new Phrase(SearchChild(table_of_button.transform,i.ToString()+"_"+j.ToString()).GetComponent<InputField>().text, new Font(baseFont, 14, Font.NORMAL, BaseColor.BLACK))));

				}
				cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
				cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
				table.AddCell(cell);
			}
			
		}
		table.HorizontalAlignment = Element.ALIGN_CENTER;
		table.SpacingAfter = 12f;
		doc.Add(table);
		Paragraph p3 = new Paragraph(" \n Вывод ",new Font(baseFont, 16, Font.BOLD, BaseColor.BLACK));
		p3.Alignment = Element.ALIGN_CENTER;
		p3.SpacingAfter = 12f;
		doc.Add(p3);
		doc.Add(new Paragraph(SearchChild(table_of_button.transform,"5mod").GetComponent<InputField>().text,
			new Font(baseFont, 14, Font.NORMAL, BaseColor.BLACK)));
		String t1 = SearchChild(table_of_button.transform, "1_0").GetComponent<InputField>().text;
		String t2 = SearchChild(table_of_button.transform, "2_0").GetComponent<InputField>().text;
		int n1,n2;
		if (int.TryParse(t1,out n1)&&int.TryParse(t2,out n2))
		{
			doc.NewPage();
			doc.Add(new Paragraph("Построенный график", new Font(baseFont, 16, Font.BOLD, BaseColor.BLACK)));
			Paragraph p10 = new Paragraph("  Вmax = "+t1+  "; Hmax = "+t2+";",new Font(baseFont, 16, Font.BOLD, BaseColor.BLACK));
			p10.Alignment = Element.ALIGN_CENTER;
			doc.Add(p10);
			Image img = Image.GetInstance(Application.dataPath+"/html.png");
 	
			float percentage = 1;
			// Это оригинальная ширина и высота картинки  
			float resizedWidht = img.Width;
			float resizedHeight = img.Height;
 	
			// В настоящее время оцениваем, больше ли ширина изображения, чем ширина страницы минус поле, если оно есть, то уменьшаем, если оно еще больше, продолжаем уменьшать,  
			// Этот процент усадки будет становиться все меньше и меньше  
			while (resizedWidht > (doc.PageSize.Width - doc.LeftMargin - doc.RightMargin) * 0.8)
			{
				percentage = percentage * 0.9f;
				resizedHeight = img.Height * percentage;
				resizedWidht = img.Width * percentage;
			}
 	
			while (resizedHeight > (doc.PageSize.Height - doc.TopMargin - doc.BottomMargin) * 0.8)
			{
				percentage = percentage * 0.9f;
				resizedHeight = img.Height * percentage;
				resizedWidht = img.Width * percentage;
			}
 	
			// Здесь мы используем рассчитанный процент, чтобы уменьшить изображение  
			img.ScalePercent(percentage * 100);
			// Позиционирование изображения, общая ширина страницы составляет 283, а высота равна 416. Если вы установите здесь 0,0, это будет левый нижний угол страницы, так что центральная точка изображения будет совпадать с центральным хранилищем страницы.  
			img.SetAbsolutePosition(doc.PageSize.Width / 2 - resizedWidht / 2, doc.PageSize.Height / 2 - resizedHeight / 2);
			writer.DirectContent.AddImage(img);
		}
		
		doc.Close();
		Application.OpenURL("file:///"+path);
    }// Update is called once per frame
    void tablet_o()
    {
	    if (CalculatorEnable)
	    {
		    CalculatorEnable = !CalculatorEnable;
		    layoutCalculator.gameObject.SetActive(CalculatorEnable);
		    tablet.transform.Translate(270, 0, 0);
	    }

	    crosshair.SetActive(!crosshair.activeSelf);
	    maincamera.cameraCanMove = !maincamera.cameraCanMove;
	    maincamera.playerCanMove = !maincamera.playerCanMove;
	    maincamera.enableHeadBob = !maincamera.enableHeadBob;
	    maincamera.GetComponent<Rigidbody>().angularVelocity = (-1)*Vector3.zero;
	    maincamera.GetComponent<Rigidbody>().velocity = (-1)*Vector3.zero;
            
	    Renderer rend = gameObject.GetComponent<Renderer>();
	    layout.SetActive(!layout.activeSelf);
	    in_tablet = !in_tablet;
	    //rend.enabled = !rend.enabled;
	    SetCursorLock(!isLocked);
    }
    public void exit()
    {
	    using_pause.SetActive(false);
	    maincamera.cameraCanMove = true;
	    maincamera.playerCanMove = true;
	    maincamera.enableHeadBob = true;
		Time.timeScale = 1;
	    SceneManager.LoadSceneAsync(0);
    }
    public void set_pause_off()
    {
	    crosshair.SetActive(true);
	    using_pause.SetActive(false);
	    maincamera.cameraCanMove = true;
	    maincamera.playerCanMove = true;
	    maincamera.enableHeadBob = true;
	    SetCursorLock(true);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) &&!using_pause.activeSelf)
        {
	        tablet_o();

        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
	        if (in_tablet )
	        {
		        tablet_o();
	        }
	        crosshair.SetActive(false);
	        SetCursorLock(!isLocked);
	        //isLocked = !isLocked;
	        maincamera.cameraCanMove = !maincamera.cameraCanMove;
	        maincamera.playerCanMove = !maincamera.playerCanMove;
	        maincamera.enableHeadBob = !maincamera.enableHeadBob;
	        maincamera.GetComponent<Rigidbody>().angularVelocity = (-1)*Vector3.zero;
	        maincamera.GetComponent<Rigidbody>().velocity = (-1)*Vector3.zero;
			using_pause.SetActive(!using_pause.activeSelf);
			if (using_pause.activeSelf)
			{
				crosshair.SetActive(false);
				SetCursorLock(false);
			}
			else
			{
				crosshair.SetActive(true);
				SetCursorLock(true);
			}
			
        }


    }
}
