using AssocSupport.Models;
using AssocSupport.Sample.Properties;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace AssocSupport.Sample
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();

            // 리소스 추출
            Directory.CreateDirectory($@"{Application.StartupPath}\Resources");
            WriteResource($@"{Application.StartupPath}\Resources\Logo.ico", Resources.Logo);
            WriteResource($@"{Application.StartupPath}\Resources\Sample.ico", Resources.Sample);
        }

        private void WriteResource(string path, Icon icon)
        {
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                icon.Save(fileStream);
            }
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            // 소프트웨어 생성
            var software = new Software
            {
                Name = "MyApp",
                CompanyName = "Kodnix",
                Description = "파일 확장자 연결 테스트 프로그램 입니다.",
                Icon = $@"{Application.StartupPath}\Resources\Logo.ico"
            };

            // 연결할 확장자 추가
            software.Identifiers.Add(new ProgrammaticID
            {
                Type = new FileType
                {
                    Extension = ".asoc",
                    ContentType = "application/sample",
                    PerceivedType = PerceivedTypes.Application
                },
                Command = new ShellCommand
                {
                    Path = Application.ExecutablePath,
                    Argument = "%1"
                },
                Description = "확장자 연결 샘플 파일",
                Icon = $@"{Application.StartupPath}\Resources\Sample.ico"
            });

            // 소프트웨어 등록
            if (AssociationUtility.Register(software))
            {
                MessageBox.Show("등록 성공", "결과", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("등록 실패", "결과", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnUnregister_Click(object sender, EventArgs e)
        {
            // 소프트웨어 해제
            if(AssociationUtility.Unregister("MyApp", "Kodnix"))
            {
                MessageBox.Show("해제 성공", "결과", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("해제 실패", "결과", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
