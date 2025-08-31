namespace MuckTrainer
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.chkGodMode = new System.Windows.Forms.CheckBox();
            this.chkInfStamina = new System.Windows.Forms.CheckBox();
            this.chkNoHunger = new System.Windows.Forms.CheckBox();
            this.chkInfJumps = new System.Windows.Forms.CheckBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.chkSuperSpeed = new System.Windows.Forms.CheckBox();
            this.chkInfShield = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // chkGodMode
            // 
            this.chkGodMode.AutoSize = true;
            this.chkGodMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.chkGodMode.Location = new System.Drawing.Point(154, 12);
            this.chkGodMode.Name = "chkGodMode";
            this.chkGodMode.Size = new System.Drawing.Size(154, 20);
            this.chkGodMode.TabIndex = 0;
            this.chkGodMode.Text = "Бессмертие [Num 1]";
            this.chkGodMode.UseVisualStyleBackColor = true;
            // 
            // chkInfStamina
            // 
            this.chkInfStamina.AutoSize = true;
            this.chkInfStamina.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.chkInfStamina.Location = new System.Drawing.Point(154, 35);
            this.chkInfStamina.Name = "chkInfStamina";
            this.chkInfStamina.Size = new System.Drawing.Size(205, 20);
            this.chkInfStamina.TabIndex = 0;
            this.chkInfStamina.Text = "Беск. выносливость [Num 2]";
            this.chkInfStamina.UseVisualStyleBackColor = true;
            // 
            // chkNoHunger
            // 
            this.chkNoHunger.AutoSize = true;
            this.chkNoHunger.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.chkNoHunger.Location = new System.Drawing.Point(154, 61);
            this.chkNoHunger.Name = "chkNoHunger";
            this.chkNoHunger.Size = new System.Drawing.Size(149, 20);
            this.chkNoHunger.TabIndex = 0;
            this.chkNoHunger.Text = "Нет голода [Num 3]";
            this.chkNoHunger.UseVisualStyleBackColor = true;
            // 
            // chkInfJumps
            // 
            this.chkInfJumps.AutoSize = true;
            this.chkInfJumps.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.chkInfJumps.Location = new System.Drawing.Point(154, 87);
            this.chkInfJumps.Name = "chkInfJumps";
            this.chkInfJumps.Size = new System.Drawing.Size(161, 20);
            this.chkInfJumps.TabIndex = 0;
            this.chkInfJumps.Text = "Беск. прыжки [Num 4]";
            this.chkInfJumps.UseVisualStyleBackColor = true;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(13, 77);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(135, 13);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "Статус: Игра не найдена ";
            // 
            // chkSuperSpeed
            // 
            this.chkSuperSpeed.AutoSize = true;
            this.chkSuperSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.chkSuperSpeed.Location = new System.Drawing.Point(154, 113);
            this.chkSuperSpeed.Name = "chkSuperSpeed";
            this.chkSuperSpeed.Size = new System.Drawing.Size(178, 20);
            this.chkSuperSpeed.TabIndex = 0;
            this.chkSuperSpeed.Text = "Супер скорость [Num 5]";
            this.chkSuperSpeed.UseVisualStyleBackColor = true;
            // 
            // chkInfShield
            // 
            this.chkInfShield.AutoSize = true;
            this.chkInfShield.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.chkInfShield.Location = new System.Drawing.Point(154, 139);
            this.chkInfShield.Name = "chkInfShield";
            this.chkInfShield.Size = new System.Drawing.Size(136, 20);
            this.chkInfShield.TabIndex = 0;
            this.chkInfShield.Text = "Беск. щит [Num 6]";
            this.chkInfShield.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 172);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.chkInfShield);
            this.Controls.Add(this.chkSuperSpeed);
            this.Controls.Add(this.chkInfJumps);
            this.Controls.Add(this.chkNoHunger);
            this.Controls.Add(this.chkInfStamina);
            this.Controls.Add(this.chkGodMode);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "MuckTrainer";
            this.Load += new System.EventHandler(this.Form1_Load_1);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkGodMode;
        private System.Windows.Forms.CheckBox chkInfStamina;
        private System.Windows.Forms.CheckBox chkNoHunger;
        private System.Windows.Forms.CheckBox chkInfJumps;
        private System.Windows.Forms.Label lblStatus;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.CheckBox chkSuperSpeed;
        private System.Windows.Forms.CheckBox chkInfShield;
    }
}

