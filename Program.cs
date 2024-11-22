using System;
using System.Drawing;
using System.Drawing.Drawing2D;

class Program
{
    static void Main()
    {
        // Dimensões da imagem
        int largura = 1080;
        int altura = 720;

        // Criar um bitmap (base da imagem)
        using (Bitmap bitmap = new Bitmap(largura, altura))
        {
            // Criar um objeto gráfico a partir do bitmap
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                // Melhorar a qualidade gráfica
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

                // Fundo com gradiente radial
                using (GraphicsPath caminhoGradiente = new GraphicsPath())
                {
                    caminhoGradiente.AddEllipse(0, 0, largura, altura);
                    using (PathGradientBrush fundoBrush = new PathGradientBrush(caminhoGradiente))
                    {
                        fundoBrush.CenterColor = Color.FromArgb(255, 100, 149, 237); // Azul claro
                        fundoBrush.SurroundColors = new Color[] { Color.FromArgb(255, 25, 25, 112) }; // Azul escuro
                        g.FillRectangle(fundoBrush, 0, 0, largura, altura);
                    }
                }

                // Desenhar bordas com gradiente
                using (LinearGradientBrush bordaBrush = new LinearGradientBrush(
                    new Rectangle(0, 0, largura, altura),
                    Color.Purple,
                    Color.Violet,
                    LinearGradientMode.ForwardDiagonal))
                {
                    using (Pen bordaPen = new Pen(bordaBrush, 15))
                    {
                        g.DrawRectangle(bordaPen, 10, 10, largura - 20, altura - 20);
                    }
                }

                // Adicionar um texto 3D
                string textoPrincipal = "Imagens com C#";
                using (Font fonte = new Font("Calibri", 70, FontStyle.Regular))
                {
                    SizeF textoTamanho = g.MeasureString(textoPrincipal, fonte);
                    PointF textoPosicao = new PointF((largura - textoTamanho.Width) / 2, 100);

                    // Sombra do texto
                    using (Brush sombraBrush = new SolidBrush(Color.FromArgb(120, 0, 0, 0)))
                    {
                        g.DrawString(textoPrincipal, fonte, sombraBrush, textoPosicao.X + 8, textoPosicao.Y + 8);
                    }

                    // Texto principal
                    using (Brush textoBrush = new LinearGradientBrush(
                        new RectangleF(textoPosicao.X, textoPosicao.Y, textoTamanho.Width, textoTamanho.Height),
                        Color.Purple,
                        Color.Violet,
                        LinearGradientMode.ForwardDiagonal))
                    {
                        g.DrawString(textoPrincipal, fonte, textoBrush, textoPosicao);
                    }
                }

                // Desenhar formas com texturas
                using (TextureBrush texturaBrush = new TextureBrush(new Bitmap("textura.png")))
                {
                    texturaBrush.WrapMode = WrapMode.Clamp;
                    g.FillEllipse(texturaBrush, new Rectangle(300, 300, 300, 300));
                }

                // Adicionar um gráfico decorativo (gráfico de barras)
                int[] valores = { 300, 300, 300, 300, 300 };
                int barraLargura = 100;
                for (int i = 0; i < valores.Length; i++)
                {
                    using (LinearGradientBrush barraBrush = new LinearGradientBrush(
                        new Rectangle(500 + i * (barraLargura + 20), altura - valores[i] - 50, barraLargura, valores[i]),
                        Color.Purple,
                        Color.Violet,
                        LinearGradientMode.Vertical))
                    {
                        g.FillRectangle(barraBrush, 500 + i * (barraLargura + 20), altura - valores[i] - 50, barraLargura, valores[i]);
                    }
                }

                // Desenhar ícones ou imagens pequenas
                using (Image icone = new Bitmap("icone.png"))
                {
                    g.DrawImage(icone, new Rectangle(largura - 150, altura - 150, 100, 100));
                }

                // Adicionar estrelas aleatórias
                Random rnd = new Random();
                for (int i = 0; i < 50; i++)
                {
                    int x = rnd.Next(50, largura - 50);
                    int y = rnd.Next(50, altura - 50);
                    using (Brush estrelaBrush = new SolidBrush(Color.White))
                    {
                        g.FillEllipse(estrelaBrush, x, y, 5, 5);
                    }
                }

                // Desenhar uma onda decorativa
                // using (Pen ondaPen = new Pen(Color.Cyan, 3))
                // {
                //     ondaPen.DashStyle = DashStyle.Dot;
                //     for (int i = 0; i < largura; i += 20)
                //     {
                //         g.DrawBezier(ondaPen, new Point(i, 800), new Point(i + 10, 780), new Point(i + 20, 820), new Point(i + 30, 800));
                //     }
                // }
            }

            // Salvar a imagem em um arquivo
            string caminho = "imagem_avancada.png";
            bitmap.Save(caminho, System.Drawing.Imaging.ImageFormat.Png);

            Console.WriteLine($"Imagem criada e salva em: {caminho}");
        }
    }
}
